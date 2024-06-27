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
        public async Task<ActionResult<SendAPI>> GetMyBankList(string userId)
        {
            try
            {
                string select = $"SELECT * FROM MEM-BANK-LINK WHERE USER_ID='{userId}'";
                DataTable dt = DBSet.Select(select);
                string data = JsonConvert.SerializeObject(dt);

                string result = "fail";
                if (!string.IsNullOrEmpty(data) && !data.Equals("[]"))
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
        public async Task<ActionResult<SendAPI>> AddMyBank(string userId, string bankId)
        {
            try
            {
                string insert = $"INSERT INTO MEM-BANK-LINK(LINK_ID, USER_ID, BANK_ID) VALUES('{Common.Common.CreateUUID()}', '{userId}', '{bankId}')";
                DBSet.NonSql(insert);

                return SendMsg.APIMsg("ok", "");
            }
            catch( Exception ex)
            {
                Logs.Exception(ex);
                return SendMsg.APIMsg("error", "");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<SendAPI>> RemoveMyBank(string userId, string bankId)
        {
            try
            {
                string delete = $"DELETE FROM MEM-BANK-LINK WHERE USER_ID='{userId}' AND BANK_ID='{bankId}'";
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
