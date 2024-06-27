using BankPresentation.Models;
using Newtonsoft.Json.Linq;
using Telerik.SvgIcons;

namespace BankPresentation.Service
{
    public class BankingService
    {
        public static string GetBankList()
        {
            try
            {
                string result = APIService.PostAPI("", "GetBankList");

                string apidata = string.Empty;
                JObject jdata = JObject.Parse(result);
                if (jdata.ContainsKey("result") && jdata["result"].ToString().Equals("ok"))
                {
                    apidata = jdata["data"].ToString();
                }

                return apidata;
            }
            catch(Exception ex)
            {
                Logs.Exception(ex);
                return null;
            }
        }

        public static string GetMyBankList(string userId)
        {
            try
            {
                string data = "{\"USER_ID\":\"" + userId + "\"}";
                Console.WriteLine(data);

                string result = APIService.PostAPI(data, "GetMyBankList");

                string apidata = string.Empty;
                JObject jdata = JObject.Parse(result);
                if (jdata.ContainsKey("result") && jdata["result"].ToString().Equals("ok"))
                {
                    apidata = jdata["data"].ToString();
                }

                return apidata;
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
                return null;
            }
        }

        public static void AddMyBank(string userId, string bankId)
        {
            try
            {
                string data = "{\"USER_ID\":\"" + userId + "\", \"BANK_ID\":\"" + bankId + "\"}";
                Console.WriteLine(data);

                string result = APIService.PostAPI(data, "AddMyBank");
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
            }
        }

        public static void RemoveBank(string bankId)
        {
            try
            {
                string data = "{\"BANK_ID\":\"" + bankId + "\"}";
                Console.WriteLine(data);

                string result = APIService.PostAPI(data, "RemoveBank");
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
            }
        }

        public static string AddBank(string bankName)
        {
            try
            {
                string data = "{\"BANK_NAME\":\"" + bankName + "\"}";
                Console.WriteLine(data);

                string result = APIService.PostAPI(data, "AddBank");

                string apidata = string.Empty;
                JObject jdata = JObject.Parse(result);
                if (jdata.ContainsKey("result") && jdata["result"].ToString().Equals("ok"))
                {
                    apidata = jdata["result"].ToString();
                }

                return apidata;
            }
            catch(Exception ex)
            {
                Logs.Exception(ex);
                return null;
            }
        }
    }
}
