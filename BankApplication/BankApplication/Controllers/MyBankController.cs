using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;

namespace BankApplication.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class MyBankController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<SendAPI>> GetMyBankList(GetMyBank mbl)
        {
            try
            {
                string select = $"SELECT * FROM MEMBANKLINK WHERE USER_ID='{mbl.USER_ID}'";
                DataTable dt = DBSet.Select(select);
                string data = JsonConvert.SerializeObject(dt);

                string result = "fail";
                if (!string.IsNullOrEmpty(data) && !data.Equals("[]") && !data.Equals("null"))
                {
                    result = "ok";
                }

                return SendMsg.APIMsg(result, data);
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
                return SendMsg.APIMsg("error", "");
            }
        }

        [HttpPut]
        public async Task<ActionResult<SendAPI>> AddMyBank(BankLink mbl)
        {
            try
            {
                string assetSelect = $"SELECT * FROM ASSETMANAGEMENT WHERE USER_ID='{mbl.USER_ID}' AND BANK_ID='{mbl.BANK_ID}'";
                DataTable dt = DBSet.Select(assetSelect);
                string assetdata = JsonConvert.SerializeObject (dt);

                Console.WriteLine(assetdata);

                string result = "fail";
                
                List<string> sqlList = new();
                    
                string linkinsert = $"INSERT INTO MEMBANKLINK(LINK_ID, USER_ID, BANK_ID) VALUES('{Common.Common.CreateUUID()}', '{mbl.USER_ID}', '{mbl.BANK_ID}')";
                sqlList.Add(linkinsert);

                if (string.IsNullOrEmpty(assetdata) || assetdata.Equals("[]") || assetdata.Equals("null"))
                {
                    string assetinsert = $"INSERT INTO ASSETMANAGEMENT(ASSET_ID, USER_ID, BANK_ID, ASSET) VALUES('{Common.Common.CreateUUID()}', '{mbl.USER_ID}', '{mbl.BANK_ID}', '{0}')";
                    sqlList.Add(assetinsert);
                }

                DBSet.DistributedTransacion(sqlList);
                result = "ok";

                return SendMsg.APIMsg(result, "");
            }
            catch( Exception ex)
            {
                Logs.Exception(ex);
                return SendMsg.APIMsg("error", "");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<SendAPI>> RemoveMyBank(BankLink mbl)
        {
            try
            {
                string delete = $"DELETE FROM MEMBANKLINK WHERE USER_ID='{mbl.USER_ID}' AND BANK_ID='{mbl.BANK_ID}'";
                DBSet.NonSql(delete);

                return SendMsg.APIMsg("ok", "");
            }
            catch(Exception ex)
            {
                Logs.Exception(ex);
                return SendMsg.APIMsg("error", "");
            }
        }
    }
}
