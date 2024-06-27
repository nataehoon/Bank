﻿using BankPresentation.Common;
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

                string result = APIService.PostAPI(data, "Login");

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
                string result = APIService.PostAPI("", "GetMemberList");
                Console.WriteLine(result);

                string returnData = string.Empty;
                JObject jdata = JObject.Parse(result);
                if (jdata.ContainsKey("data"))
                {
                    returnData = jdata["data"].ToString();
                }
                else
                {
                    returnData = string.Empty;
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
