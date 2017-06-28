using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

using RepoVKR.Models;
using System.Data;
using System.Data.SqlTypes;
using System.DirectoryServices.AccountManagement;

namespace RepoVKR
{
    public class Util
    {
        private static string _connString { get; set; }
        private static Dictionary<string, string> dicCachedADUserNames;
        private static Dictionary<string, string> dicCachedAccepted;

        static Util()
        {
            _connString = new RepoVKREntities().Database.Connection.ConnectionString;
            dicCachedADUserNames = new Dictionary<string, string>();
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

        public static string GetUserName()
        {
            return GetADUserName(System.Environment.UserName);
        }
        public static string GetADUserName(string userName)
        {
            try
            {
                if (dicCachedADUserNames.ContainsKey(userName))
                    return dicCachedADUserNames[userName];

                var ADPrincipal = new PrincipalContext(ContextType.Domain);
                UserPrincipal user = UserPrincipal.FindByIdentity(ADPrincipal, userName);

                string sDisplayName = user.DisplayName;
                dicCachedADUserNames.Add(userName, sDisplayName);

                if (user != null)
                    return sDisplayName;
            }
            catch { }

            return userName;
        }

        public static void LogGraduateBookActionToDB(Guid GraduateBookId, string ActionName, string userName)
        {
            ActionLOG logEntry = new ActionLOG();
            logEntry.TIMESTAMP = DateTime.Now;
            logEntry.GraduateBookId = GraduateBookId;
            logEntry.ActionUser = Util.GetADUserName(userName);
            logEntry.ActionName = ActionName;

            using (RepoVKREntities context = new RepoVKREntities())
            {
                context.ActionLOG.Add(logEntry);
                context.SaveChanges();
            }
        }

        public static bool GetIsUserInAccepted(string sUserName)
        {
            if (string.IsNullOrEmpty(sUserName))
                return false;

            if (sUserName.StartsWith("RECTORAT\\"))
                sUserName = sUserName.Substring(9);

            if (dicCachedAccepted.ContainsKey(sUserName))
                return true;

            using (RepoVKREntities context = new RepoVKREntities())
            {
                int cnt = context.AcceptedLogins.Where(x => x.Login == sUserName).Count();
                if (cnt == 0)
                    cnt += context.PrivilegedLogins.Where(x => x.Login == sUserName).Count();

                if (cnt > 0)
                    dicCachedAccepted.Add(sUserName, "1");

                return cnt > 0;
            }
        }
    }
}