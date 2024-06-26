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
        public static string PostAPI(string apiName, string data, string serverInfo)
        {
            try
            {
                APIServerInfo serverinfo = JsonConvert.DeserializeObject<APIServerInfo>(serverInfo);

                string serverUrl = $"http://{serverinfo.SERVERIP}:{serverinfo.SERVERPORT}/api/{apiName}";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);
                request.Method = "POST";
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
