using System.Reflection;

namespace BankApplication
{
    public class Logs
    {
        private static string appStartPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static void Debug(string msg)
        {
            try
            {
                string logDate = DateTime.Now.ToString("G");
                string logNameDay = DateTime.Now.ToString("yyyyMMdd");
                string logDir = appStartPath + @"\Logs";
                DirectoryInfo diLogs = new DirectoryInfo(logDir);

                if (!diLogs.Exists) diLogs.Create();

                using (StreamWriter outputFile = new StreamWriter(logDir + @"\Debug_Service_" + logNameDay, true))
                {
                    outputFile.WriteLine("[" + logDate + "]" + msg);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public static void Exception(Exception msg)
        {
            string logDate = DateTime.Now.ToString("G");
            string logNameDay = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
            string logDir = appStartPath + @"\Logs\Exception";
            DirectoryInfo diEx = new DirectoryInfo(logDir);
            try
            {
                if (!diEx.Exists) diEx.Create();
                using (StreamWriter outputFile = new StreamWriter(logDir + @"\EX_" + logNameDay, true))
                {
                    outputFile.WriteLine("[" + logDate + "]" + msg);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
