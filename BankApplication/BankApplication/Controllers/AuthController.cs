﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;
using System.Text.Json.Serialization;

namespace BankApplication.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BankPAPI _context;

        public AuthController(BankPAPI context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<SendAPI>> GetMemberList()
        {
            try
            {
                string select = "SELECT * FROM MEMBERS";
                DataTable dt = DBSet.Select(select);
                string memberList = JsonConvert.SerializeObject(dt);

                return SendMsg.APIMsg("ok", memberList);
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
                return SendMsg.APIMsg("error", "");
            }
        }

        [HttpPut]
        public async Task<ActionResult<SendAPI>> AddMember(Member member)
        {
            try
            {
                string insert = $"INSERT INTO MEMBERS(ID, NAME, PW, EMAIL, IMG_URL) VALUES('{member.ID}', '{member.NAME}', '{member.PW}', '{member.EMAIL}', '{member.IMG_URL}')";
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
        public async Task<ActionResult<SendAPI>> UpdateMember(Member member)
        {
            try
            {
                string update = $"UPDATE MEMBERS SET NAME='{member.NAME}', PW='{member.PW}', EMAIL='{member.EMAIL}', IMG_URL='{member.IMG_URL}' WHERE ID='{member.ID}'";
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
        public async Task<ActionResult<SendAPI>> DeleteMember(Member member)
        {
            try
            {
                string delete = $"DELETE FROM MEMBERS WHERE ID='{member.ID}'";
                DBSet.NonSql(delete);

                return SendMsg.APIMsg("ok", "");
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
                return SendMsg.APIMsg("error", "");
            }
        }
    }
}
