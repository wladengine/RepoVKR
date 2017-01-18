using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace RepoVKR
{
    public class SQLVarbinaryStreamClass
    {
        private SqlConnection _Connection;

        private string _TableName;
        private string _BinaryColumn;
        private string _KeyColumn;

        public SQLVarbinaryStreamClass(
            string ConnectionString,
            string TableName,
            string BinaryColumn,
            string KeyColumn)
        {
            // create own connection with the connection string.

            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder(ConnectionString);
            //scsb.Pooling = true;
            //scsb.AsynchronousProcessing = true;
            scsb.ConnectTimeout = 60 * 5;
            _Connection = new SqlConnection(scsb.ConnectionString);

            _TableName = TableName;
            _BinaryColumn = BinaryColumn;
            _KeyColumn = KeyColumn;
        }

        // this method will be called as part of the Stream ímplementation when we try to write to our VarbinaryStream class.
        public void Write(FileStream fs, Guid id, int chunkSize = 4 * 1024 * 1024)
        {
            try
            {
                if (_Connection.State != ConnectionState.Open)
                    _Connection.Open();

                int fsPos = 0;
                long fsElapsed = fs.Length;

                do
                {
                    if (fsElapsed < chunkSize)
                        chunkSize = (int)fs.Length;

                    byte[] buffer = new byte[chunkSize];
                    fs.Read(buffer, fsPos, chunkSize);

                    if (fsPos == 0)
                    {
                        // for the first write we just send the bytes to the Column
                        SqlCommand cmd =
                            new SqlCommand(@"UPDATE [" + _TableName + @"]\n" +
                                            "SET [" + _BinaryColumn + @"] = @firstchunk \n" +
                                            "WHERE [" + _KeyColumn + "] = @id",
                                                _Connection);

                        cmd.Parameters.Add(new SqlParameter("@firstchunk", buffer));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // for all updates after the first one we use the TSQL command .WRITE() to append the data in the database
                        SqlCommand cmd =
                            new SqlCommand(@"UPDATE [" + _TableName + @"] \n" +
                                            "SET [" + _BinaryColumn + @"].WRITE(@chunk, NULL, @length) \n" +
                                            "WHERE [" + _KeyColumn + "] = @id",
                                             _Connection);

                        cmd.Parameters.Add(new SqlParameter("@chunk", buffer));
                        cmd.Parameters.Add(new SqlParameter("@length", fsPos));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        cmd.ExecuteNonQuery();
                    }

                    fsPos += chunkSize;
                    fsElapsed -= chunkSize;
                }
                while (fsElapsed >= 0);
            }
            catch (Exception e)
            {
                // log errors here
            }
        }

        //
        public async Task CopyBinaryValueToFile(string filePath, Guid id)
        {
            if (_Connection.State != ConnectionState.Open)
                await _Connection.OpenAsync();

            string SqlQuery = @"SELECT [" + _BinaryColumn + "] FROM [" + _TableName + "] WHERE [" + _KeyColumn + "] = @id";

            using (SqlCommand command = new SqlCommand(SqlQuery, _Connection))
            {
                command.Parameters.AddWithValue("id", id);

                // The reader needs to be executed with the SequentialAccess behavior to enable network streaming
                // Otherwise ReadAsync will buffer the entire BLOB into memory which can cause scalability issues or even OutOfMemoryExceptions
                using (SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess))
                {
                    if (await reader.ReadAsync())
                    {
                        if (!(await reader.IsDBNullAsync(0)))
                        {
                            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                            {
                                using (Stream data = reader.GetStream(0))
                                {
                                    // Asynchronously copy the stream from the server to the file we just created
                                    await data.CopyToAsync(file);
                                }
                            }
                        }
                    }
                }
            }
        }

        // Application transferring a large BLOB to SQL Server in .Net 4.5
        public async Task InsertStreamBLOBToServer(FileStream file, Guid id, CancellationToken cancellationToken)
        {
            if (_Connection.State != ConnectionState.Open)
                await _Connection.OpenAsync();

            using (SqlCommand cmd = new SqlCommand("INSERT INTO [" + _TableName + "] (" + _KeyColumn + ", " + _BinaryColumn + ") VALUES (@id, @bindata)", _Connection))
            {
                // Add a parameter which uses the FileStream we just opened
                // Size is set to -1 to indicate "MAX"
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
                cmd.Parameters.Add("@bindata", SqlDbType.Binary, -1).Value = file;

                // Send the data to the server asynchronously
                await cmd.ExecuteNonQueryAsync(cancellationToken);
            }

            _Connection.Close();
        }

        public async Task UpdateStreamBLOBToServer(FileStream file, Guid id, CancellationToken cancellationToken)
        {
            if (_Connection.State != ConnectionState.Open)
                await _Connection.OpenAsync();

            using (SqlCommand cmd = new SqlCommand("UPDATE [" + _TableName + "] SET [" + _BinaryColumn + "]=@bindata WHERE [" + _KeyColumn + "]=@id", _Connection))
            {
                // Add a parameter which uses the FileStream we just opened
                // Size is set to -1 to indicate "MAX"
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
                cmd.Parameters.Add("@bindata", SqlDbType.Binary, -1).Value = file;

                // Send the data to the server asynchronously
                await cmd.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        public async Task DeleteBLOBFromServer(Guid id, CancellationToken cancellationToken)
        {
            if (_Connection.State != ConnectionState.Open)
                await _Connection.OpenAsync();

            using (SqlCommand cmd = new SqlCommand("DELETE FROM [" + _TableName + "] WHERE [" + _KeyColumn + "]=@id", _Connection))
            {
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;

                // Send the data to the server asynchronously
                await cmd.ExecuteNonQueryAsync(cancellationToken);
            }
        }
    }
}