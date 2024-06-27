using System.Data;
using System.Data.SqlClient;

namespace BankApplication.Database
{
    public class MSSql
    {
        private static string server = "";
        private static string port = "";
        private static string dbname = "";
        private static string id = "";
        private static string pwd = "";

        private static string strConn = $"server={server},{port};database={dbname};uId={id};pwd={pwd};";

        public static DataTable Select(string sql)
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    adpt.Fill(dt);
                }

                return dt;
            }
            catch(Exception ex)
            {
                Logs.Exception(ex);
                return null;
            }
        }

        public static void NonSql(string sql)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
            }
        }
    }
}
