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

        // 로그인 및 Member 관련 DB Connection모음
        LoginConnection loginConn = new LoginConnection();
        // 회원삭제 및 기타 삭제 관련
        RemoveDB deleteMethod = new RemoveDB();
        // 점검 관련 함수들 모음
        Check check = new Check();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult MemberInfo()
        {
            return View();
        }

        //로그인 액션
        [HttpPost]
        // POST : ~~ /Account/BeginLogin
        [ActionName("BeginLogin")]
        public ActionResult BeginLogin()
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
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        // 로그아웃 액션
        // POST : ~~/Account/BeginLogout
        [ActionName("BeginLogout")]
        public ActionResult BeginLogout()
        {
            try
            {
                // 세션을 초기화 한다.
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // 회원 탈퇴 액션
        [ActionName("MemberDrop")]
        public ActionResult MemberDrop()
        {
            try
            {
                // 세션에 값이 없을 경우 에러페이지로 이동
                if(Session["MemberID"] == null)
                {
                    throw new Exception();
                }
                bool queryResult = deleteMethod.MemberDrop(Convert.ToInt32(Session["MemberID"]));
                // 로그인이 실패할 경우 에러페이지로 이동
                if(queryResult == false)
                {
                    throw new Exception();
                }
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("CommonError", "Home");
            }
        }
    }
}