﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BankApplication.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<SendAPI>> GetBankList()
        {
            try
            {
                string select = "SELECT * FROM BANK";
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
        public async Task<ActionResult<SendAPI>> AddBank(string bankName)
        {
            try
            {
                string insert = $"INSERT INTO BANK(BANK_ID, BANK_NAME) VALUES('{Common.Common.CreateUUID()}', '{bankName}')";
                DBSet.NonSql(insert);

                return SendMsg.APIMsg("ok", "");
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
                return SendMsg.APIMsg("error", "");
            }
        }

        [HttpPatch]
        public async Task<ActionResult<SendAPI>> UpdateBank(string bankId, string bankName)
        {
            try
            {
                string update = $"UPDATE BANK SET BANK_NAME='{bankName}' WHERE BANK_ID='{bankId}'";
                DBSet.NonSql(update);

                return SendMsg.APIMsg("ok", "");
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
                return SendMsg.APIMsg("error", "");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<SendAPI>> RemoveBank(string bankId)
        {
            try
            {
                List<string> sqlList = new();
                string bankdelete = $"DELETE FROM BANK WHERE BANK_ID='{bankId}'";
                sqlList.Add(bankdelete);

                string assetselect = $"SELECT * FROM ASSETMANAGEMENT WHERE BANK_ID='{bankId}'";
                DataTable dt2 = DBSet.Select(assetselect);
                string assetdata = JsonConvert.SerializeObject(dt2);
                if (!string.IsNullOrEmpty(assetdata) && !assetdata.Equals("[]") && !assetdata.Equals("null"))
                {
                    string assetdelete = $"DELETE FROM ASSETMANAGEMENT WHERE BANK_ID='{bankId}'";
                    sqlList.Add(assetdelete);
                }

                string linkselect = $"SELECT * FROM MEM-BANK-LINK WHERE BANK_ID='{bankId}'";
                DataTable dt = DBSet.Select(linkselect);
                string linkdata = JsonConvert.SerializeObject(dt);
                if (!string.IsNullOrEmpty(linkdata) && !linkdata.Equals("[]") && !linkdata.Equals("null"))
                {
                    string linkdelete = $"DELETE FROM MEM-BANK-LINK WHERE BANK_ID='{bankId}'";
                    sqlList.Add(linkdelete);
                }

                DBSet.DistributedTransacion(sqlList);

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
