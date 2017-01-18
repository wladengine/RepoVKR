using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoVKR_WinApp
{
    internal static class DbOperations
    {

        #region General
        
        /// <summary>
        /// Opens and returns a connection to a database
        /// </summary>
        /// <returns>An open connection</returns>
        private static string GetConnectionString(DbContext context)
        {
            return context.Database.Connection.ConnectionString;
        }

        #endregion General

        #region Uploading
        /// <summary>
        /// Stores the file to the database using SqlFileStream
        /// </summary>
        /// <param name="file">File to save</param>
        /// <param name="repeatCount">How many times to save</param>
        /// <returns>Elapsed time</returns>
        internal static void StoreFileUsingSqlFileStream(FileInfo file, DbContext context)
        {
            object transactionContext;
            System.Data.SqlClient.SqlParameter parameter;
            System.Data.SqlTypes.SqlFileStream sqlFileStream;
            byte[] fileData;
            string filePathInServer;
            int rowsInserted;

            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(DbOperations.GetConnectionString(context)))
            {
                connection.Open();
                using (System.Data.SqlClient.SqlCommand insertCommand = connection.CreateCommand())
                {
                    using (System.Data.SqlClient.SqlCommand helperCommand = connection.CreateCommand())
                    {
                        insertCommand.CommandText = "INSERT INTO FileStreamTest ([Id], [Name], [Data]) VALUES (@Id, @Name, (0x))";
                        insertCommand.CommandType = System.Data.CommandType.Text;

                        parameter = new System.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.UniqueIdentifier);
                        insertCommand.Parameters.Add(parameter);

                        parameter = new System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 100);
                        parameter.Value = file.Name;
                        insertCommand.Parameters.Add(parameter);

                        insertCommand.Transaction = connection.BeginTransaction();
                        helperCommand.Transaction = insertCommand.Transaction;

                        helperCommand.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()";
                        transactionContext = helperCommand.ExecuteScalar();

                        helperCommand.CommandText = "SELECT Data.PathName() FROM FileStreamTest WHERE [Id] = @Id";
                        parameter = new System.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.UniqueIdentifier);
                        helperCommand.Parameters.Add(parameter);

                        fileData = System.IO.File.ReadAllBytes(file.FullName);
                        
                        insertCommand.Parameters["@Id"].Value = System.Guid.NewGuid();
                        rowsInserted = insertCommand.ExecuteNonQuery();

                        helperCommand.Parameters["@Id"].Value = insertCommand.Parameters["@Id"].Value;
                        filePathInServer = (string)helperCommand.ExecuteScalar();

                        sqlFileStream = new System.Data.SqlTypes.SqlFileStream(filePathInServer, (byte[])transactionContext, System.IO.FileAccess.Write);
                        sqlFileStream.Write(fileData, 0, fileData.Length);
                        sqlFileStream.Close();

                        insertCommand.Transaction.Commit();
                    }
                }
                connection.Close();
            }
        }

        #endregion Uploading
    }
}
