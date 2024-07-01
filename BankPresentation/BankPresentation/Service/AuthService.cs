using BankPresentation.Common;
using BankPresentation.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telerik.SvgIcons;

namespace BankPresentation.Service
{
    public class AuthService
    {
        public static string Login(string id, string pw)
        {
            try
            {
                string data = "{ \"ID\":\"" + id + "\", \"PW\":\"" + pw + "\"}";

                string result = APIService.API(data, "Login", "POST");

                if (!string.IsNullOrEmpty(result))
                {
                    string logindata = string.Empty;
                    JObject jdata = JObject.Parse(result);
                    if (jdata.ContainsKey("result"))
                    {
                        logindata = jdata["result"].ToString();
                    }
                    return logindata;
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

        public static string GetMemberList()
        {
            try
            {
                string result = APIService.API("", "GetMemberList", "POST");

                string returnData = string.Empty;
                JObject jdata = JObject.Parse(result);
                if (jdata.ContainsKey("result") && jdata["result"].ToString().Equals("ok"))
                {
                    returnData = jdata["data"].ToString();
                }

                return returnData;
            }
            catch(Exception ex)
            {
                Logs.Exception(ex);
                return null;
            }
        }
    }
}
