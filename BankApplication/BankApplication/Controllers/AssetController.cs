using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BankApplication.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        [HttpPatch]
        public async Task<ActionResult<SendAPI>> UpdateAsset(string fromUser, string frombank, string toUser, string tobank, string asset) // 송금
        {
            try
            {
                string select = "SELECT * FROM ASSETMANAGEMENT";
                DataTable dt = DBSet.Select(select);
                string selectdata = JsonConvert.SerializeObject(dt);

                string result = "fail";
                if (!string.IsNullOrEmpty(selectdata) && !selectdata.Equals("[]"))
                {
                    List<AssetManagement> memBankLink = JsonConvert.DeserializeObject<List<AssetManagement>>(selectdata);

                    //보낸사람
                    var fromuserdata = memBankLink.FirstOrDefault(x => x.USER_ID.Equals(fromUser) && x.BANK_ID.Equals(frombank));

                    string fromasset = (Convert.ToInt32(fromuserdata.ASSET) - Convert.ToInt32(asset)).ToString(); // 보낸금액 뺴기
                    string fromupdate = $"UPDATE ASSETMANAGEMENT SET Asset='{fromasset}' WHERE USER_ID='{fromUser}' AND BANK_ID='{frombank}'";

                    //받는사람
                    var touserdata = memBankLink.First(x => x.USER_ID.Equals(toUser) && x.BANK_ID.Equals(tobank));

                    string toasset = (Convert.ToInt32(touserdata.ASSET) + Convert.ToInt32(asset)).ToString(); // 받은금액 더하기
                    string toupdate = $"UPDATE ASSETMANAGEMENT SET Asset='{toasset}' WHERE USER_ID='{toUser}' AND BANK_ID='{tobank}'";

                    List<string> sqlList = new();
                    sqlList.Add(fromupdate);
                    sqlList.Add(toupdate);

                    DBSet.DistributedTransacion(sqlList);

                    result = "ok";
                }

                return SendMsg.APIMsg(result, "");
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
                return SendMsg.APIMsg("error", "");
            }
        }
    }
}
