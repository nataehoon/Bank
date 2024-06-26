using System.Reflection;

namespace BankPresentation.Common
{
    public class Logs
    {
        public static bool debugSet = false;
        public static string appStartPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static void Info(string msg)
        {
            string logDate = DateTime.Now.ToString("G");
            string logNameDay = DateTime.Now.ToString("yyyyMMdd");
            string logDir = appStartPath + @"\Logs";
            DirectoryInfo diLogs = new DirectoryInfo(logDir);
            try
            {
                if (!diLogs.Exists) diLogs.Create();
                using (StreamWriter outputFile = new StreamWriter(logDir + @"\Service_" + logNameDay + ".log", true))
                {
                    outputFile.WriteLine("[" + logDate + "] " + msg);
                }
            }
            catch { }
        }

        /*public static bool DebugSet()
        {
            string debug = Common.getINIValue("Logs", "debug");
            if (debug.Equals("1"))
                return true;
            else
                return false;
        }*/

        public static void Debug(string msg)
        {
            /*debugSet = DebugSet();
            if (!debugSet) return;*/

            string logDate = DateTime.Now.ToString("G");
            string logNameDay = DateTime.Now.ToString("yyyyMMdd");
            string logDir = appStartPath + @"\Logs";
            DirectoryInfo diLogs = new DirectoryInfo(logDir);
            try
            {
                if (!diLogs.Exists) diLogs.Create();

                using (StreamWriter outputFile = new StreamWriter(logDir + @"\Debug_Service_" + logNameDay, true))
                {
                    outputFile.WriteLine("[" + logDate + "] " + msg);
                }
            }
            catch { }
        }

        public static void Exception(Exception msg)
        {
            string logDate = DateTime.Now.ToString("G");
            string logName = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
            string logDir = appStartPath + @"\Logs\Exception\";
            DirectoryInfo diLogs = new DirectoryInfo(logDir);
            try
            {
                if (!diLogs.Exists)
                    diLogs.Create();

                using (StreamWriter outputFile = new StreamWriter(logDir + "EX_" + logName, true))
                {
                    outputFile.WriteLine("[" + logDate + "] " + msg);
                }
            }
            catch { }
        }
    }
}
