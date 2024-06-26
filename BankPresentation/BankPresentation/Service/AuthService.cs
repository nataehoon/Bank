using BankPresentation.Common;
using BankPresentation.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankPresentation.Service
{
    public class AuthService
    {
        public static string Login(string id, string pw)
        {
            try
            {
                APIServerInfo apiinfo = new();
                apiinfo.SERVERIP = "";
                apiinfo.SERVERPORT = "";

                string data = "{ \"ID\":" + id + ", \"PW\":" + pw + "}";

                string result = APIService.PostAPI("Login", data, JsonConvert.SerializeObject(apiinfo));

                JObject jdata = JObject.Parse(result);
                if (jdata.ContainsKey("data"))
                {
                    return jdata["data"].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
                return null;
            }
        }
    }
}
