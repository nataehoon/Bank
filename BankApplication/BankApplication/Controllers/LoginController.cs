using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BankApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BankPAPI _context;

        public LoginController(BankPAPI context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<SendAPI>> Login(Member logindata)
        {
            try
            {
                string select = "SELECT * FROM MEMBERS";
                DataTable dt = DBSet.Select(select);
                string selectData = JsonConvert.SerializeObject(dt);
                List<Member> memberList = JsonConvert.DeserializeObject<List<Member>>(selectData);

                string result = "fail";
                foreach (var member in memberList)
                {
                    if (member.ID.Equals(logindata.ID))
                    {
                        if (member.PW.Equals(logindata.PW))
                        {
                            result = "ok";
                        }
                    }
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
