using CatHouse_Renewal.Models;
using CatHouse_Renewal.DB;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace CatHouse_Renewal.Controllers
{
    public class AccountController : Controller
    {

        // 로그인 관련 DB Connection모음
        LoginConnection loginConn = new LoginConnection();
        // 점검 관련 함수들 모음
        Check check = new Check();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("BeginLogin")]
        public void BeginLogin(MemberModel memModel)
        {
            try
            {
                string memEmail = Request.Form["loginMemberId"];
                string memPassword = Request.Form["loginMemberPassword"];

                DataSet queryResult = loginConn.MemberLogin(memEmail, memPassword);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            // 로그인 확인
            //if(ModelState.IsValid == true)
            //{
            //    int returnValue = 0;
            //    if(returnValue == 0)
            //    {
            //        //로그인 성공
            //        Session[claGlobal.SessionString_UserID] = memModel.memID;
            //    }
            //}
        }
    }
}