using System.Data;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.Reflection;
using System.Transactions;

namespace BankApplication.Database
{
    public class SQLite
    {
        private static string appStartPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        private static string dbLogPath = appStartPath + "\\DB\\BankWep.db";
        private static string connstring = $"Data Source={dbLogPath};Version=3;";

        public static DataTable Select(string sql)
        {
            try
            {
                DataTable dt = new DataTable();

                SQLiteConnection conn = new SQLiteConnection(connstring);
                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataAdapter adpt = new SQLiteDataAdapter(cmd);
                adpt.Fill(dt);

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
                SQLiteConnection conn = new SQLiteConnection(connstring);
                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
            }
        }

        public static void DistributedTransacion(List<string> sqlList)
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection(connstring);
                conn.Open();

                SQLiteTransaction transaction = conn.BeginTransaction();
                try
                {
                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.Transaction = transaction;

                    foreach(var item in sqlList)
                    {
                        cmd.CommandText = item;
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Logs.Exception(ex);
                    transaction.Rollback();
                }
            }
            catch(Exception ex)
            {
                Logs.Exception(ex);
            }
        }
    }
}
