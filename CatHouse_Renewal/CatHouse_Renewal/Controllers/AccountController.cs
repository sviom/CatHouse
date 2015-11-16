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
        public JavaScriptResult BeginLogin(MemberModel memModel)
        {
            try
            {
                // 해당 항목으로부터 값 얻어오기
                string memEmail = Request.Form["loginMemberId"];
                string memPassword = Request.Form["loginMemberPassword"];

                // 로그인 시도
                int loginQueryMemID = loginConn.MemberLogin(memEmail, memPassword);
                if (loginQueryMemID <= 0)
                {
                    // 로그인 하려고 하는데 자료가 없거나 오류이면 에러
                    throw new Exception();
                }

                // 관련 항목(로그인버튼/이름) 매칭
                Session["MemberID"] = loginQueryMemID;


                return JavaScript("showPopup('Success','성공입니다.',400,400);");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
    }
}