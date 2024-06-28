using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BankApplication.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<SendAPI>> GetMyAssetList()
        {
            try
            {
                string select = $"SELECT * FROM ASSETMANAGEMENT";
                DataTable dt = DBSet.Select(select) ;
                string data = JsonConvert.SerializeObject(dt);

                string result = "fail";
                if(!string.IsNullOrEmpty(data) && !data.Equals("[]") && !data.Equals("null"))
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

        [HttpPatch]
        public async Task<ActionResult<SendAPI>> AddAsset(Asset asset) // 송금
        {
            try
            {
                string select = $"SELECT * FROM ASSETMANAGEMENT WHERE USER_ID='{asset.USER_ID}' AND BANK_ID='{asset.BANK_ID}'";
                DataTable dt = DBSet.Select(select);
                string data = JsonConvert.SerializeObject(dt);

                string result = "fail";
                if(!string.IsNullOrEmpty(data) && !data.Equals("[]") && !data.Equals("null"))
                {
                    AssetManagement assetinfo = JsonConvert.DeserializeObject<List<AssetManagement>>(data)[0];

                    string myasset = (Convert.ToInt32(assetinfo.ASSET) + Convert.ToInt32(asset.ASSET)).ToString();
                    string insert = $"UPDATE ASSETMANAGEMENT SET ASSET='{myasset}' WHERE USER_ID='{asset.USER_ID}' AND BANK_ID='{asset.BANK_ID}'";
                    DBSet.NonSql(insert);

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

        [HttpPatch]
        public async Task<ActionResult<SendAPI>> UpdateAsset(SendAsset asset) // 이체
        {
            try
            {
                string select = "SELECT * FROM ASSETMANAGEMENT";
                DataTable dt = DBSet.Select(select);
                string selectdata = JsonConvert.SerializeObject(dt);

                string result = "fail";
                if (!string.IsNullOrEmpty(selectdata) && !selectdata.Equals("[]") && !selectdata.Equals("null"))
                {
                    List<AssetManagement> memBankLink = JsonConvert.DeserializeObject<List<AssetManagement>>(selectdata);

                    //보낸사람
                    var fromuserdata = memBankLink.FirstOrDefault(x => x.USER_ID.Equals(asset.SEND_USER) && x.BANK_ID.Equals(asset.SEND_BANK));

                    string fromasset = (Convert.ToInt32(fromuserdata.ASSET) - Convert.ToInt32(asset.ASSET) - Convert.ToInt32(asset.CHARGE)).ToString(); // 보낸금액 뺴기
                    string fromupdate = $"UPDATE ASSETMANAGEMENT SET Asset='{fromasset}' WHERE USER_ID='{asset.SEND_USER}' AND BANK_ID='{asset.SEND_BANK}'";

                    //받는사람
                    var touserdata = memBankLink.First(x => x.USER_ID.Equals(asset.RECEIVE_USER) && x.BANK_ID.Equals(asset.RECEIVE_BANK));

                    string toasset = (Convert.ToInt32(touserdata.ASSET) + Convert.ToInt32(asset.ASSET)).ToString(); // 받은금액 더하기
                    string toupdate = $"UPDATE ASSETMANAGEMENT SET Asset='{toasset}' WHERE USER_ID='{asset.RECEIVE_USER}' AND BANK_ID='{asset.RECEIVE_BANK}'";

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
