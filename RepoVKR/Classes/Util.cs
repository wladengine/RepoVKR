using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

using RepoVKR.Models;
using System.Data;
using System.Data.SqlTypes;

namespace RepoVKR
{
    public class Util
    {
        private static string _connString { get; set; }

        static Util()
        {
            _connString = new RepoVKREntities().Database.Connection.ConnectionString;
        }

        public static Stream GetStreamSQL(string tableName, string coumnDataName, Guid Id)
        {
            SqlConnection _Connection = new SqlConnection(_connString);
            _Connection.Open();

            string query = "SELECT [" + coumnDataName + "] FROM [" + tableName + "] WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(query, _Connection);

            SqlParameter paramId = new SqlParameter(@"Id", SqlDbType.UniqueIdentifier);
            paramId.Value = Id;
            cmd.Parameters.Add(paramId);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
            if (reader.Read())
            {
                return reader.GetStream(0);
            }

            return null;
        }
    }
}