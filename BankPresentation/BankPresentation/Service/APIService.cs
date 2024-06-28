using BankPresentation.Common;
using BankPresentation.Models;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Text;

namespace BankPresentation.Service
{
    public class APIService
    {
        private static string serverip = "localhost";
        private static string serverport = "7275";

        private static string baseServerUrl = $"https://{serverip}:{serverport}/api/";
        public static string API(string data, string apiname, string method)
        {
            try
            {
                string serverUrl = baseServerUrl + apiname;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);
                request.Method = method;
                request.ContentType = "application/json";
                request.Timeout = 30 * 1000;

                // POST할 데이타를 Request Stream에 쓴다
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                request.ContentLength = bytes.Length; // 바이트수 지정

                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bytes, 0, bytes.Length);
                }

                // Response 처리
                string responseText = string.Empty;
                using (WebResponse resp = request.GetResponse())
                {
                    Stream respStream = resp.GetResponseStream();
                    using (StreamReader sr = new StreamReader(respStream))
                    {
                        responseText = sr.ReadToEnd();
                    }
                }

                return responseText;
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
                return null;
            }
        }
    }
}
