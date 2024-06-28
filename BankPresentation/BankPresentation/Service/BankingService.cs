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
                string result = APIService.API("", "GetBankList", "POST");

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

                string result = APIService.API(data, "GetMyBankList", "POST");

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

                string result = APIService.API(data, "AddMyBank", "PUT");
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

                string result = APIService.API(data, "RemoveBank", "DELETE");
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

                string result = APIService.API(data, "AddBank", "PUT");

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

        public static string RemoveMyBank(string userId, string bankId)
        {
            try
            {
                string data = "{\"USER_ID\":\"" + userId + "\", \"BANK_ID\":\"" + bankId + "\"}";

                string result = APIService.API(data, "RemoveMyBank", "DELETE");

                string apidata = string.Empty;
                JObject jdata = JObject.Parse(result);
                if (jdata.ContainsKey("result") && jdata["result"].ToString().Equals("ok"))
                {
                    apidata = jdata["result"].ToString();
                }

                return apidata;
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
                return null;
            }
        }

        public static string SendAsset(string fromUser, string fromBank, string toUser, string toBank, int asset, int charge)
        {
            try
            {
                string data = "{\"SEND_USER\": \"" + fromUser + "\", \"SEND_BANK\": \"" + fromBank + "\", \"RECEIVE_USER\": \"" + toUser + "\", \"RECEIVE_BANK\": \"" + toBank + "\", \"ASSET\": \"" + asset + "\", \"CHARGE\": \"" + charge + "\"}";

                string result = APIService.API(data, "UpdateAsset", "PATCH");

                string apidata = string.Empty;
                JObject jdata = JObject.Parse(result);
                if (jdata.ContainsKey("result") && jdata["result"].ToString().Equals("ok"))
                {
                    apidata = jdata["result"].ToString();
                }

                return apidata;
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
                return null;
            }
        }

        public static string GetMyAssetList()
        {
            try
            {
                string result = APIService.API("", "GetMyAssetList", "POST");

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
    }
}
