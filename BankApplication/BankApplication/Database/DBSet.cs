using System.Data;

namespace BankApplication.Database
{
    public class DBSet
    {
        private static string dbType = "SQLite";

        public static DataTable Select(string select)
        {
            try
            {
                DataTable dt = new DataTable();
                if (dbType.Equals("SQLite"))
                {
                    dt = SQLite.Select(select);
                }

                return dt;
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
                return null;
            }
        }

        public static void NonSql(string sql)
        {
            try
            {
                if (dbType.Equals("SQLite"))
                {
                    SQLite.NonSql(sql);
                }
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
            }
        }
    }
}
