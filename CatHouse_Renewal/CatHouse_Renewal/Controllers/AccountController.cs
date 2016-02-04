using CatHouse_Renewal.Models;
using CatHouse_Renewal.DB;
using CatHouse_Renewal.Other;
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
        // 회원 정보 및 기타 업데이트 관련 
        UpdateDB updateMethod = new UpdateDB();
        // 정보 SELECT 관련
        SelectDB selectMethod = new SelectDB();
        // 점검 관련 함수들 모음
        Check check = new Check();

        // 로그인 페이지로 이동
        public ActionResult Login()
        {
            return View();
        }

        // 개인정보 페이지로 이동
        public ActionResult MemberInfo()
        {
            return View();
        }

        // 비밀번호 변경 페이지로 이동
        public ActionResult PasswordChange()
        {
            return View();
        }

        // 개인정보 변경 페이지로 이동
        public ActionResult InfoChange()
        {
            try
            {
                HomeModel memberAddress = selectMethod.GetMemberAddress(Convert.ToInt32(Session["MemberID"]), Session["MemberEmail"].ToString());
                return View(memberAddress);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return View();
            }
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
                LoginConnection.SessionData loginQuery = loginConn.MemberLogin(memEmail, memPassword);
                if (loginQuery.MemberID <= 0)
                {
                    // 로그인 하려고 하는데 자료가 없거나 오류이면 에러
                    throw new Exception();
                }

                // 관련 항목(로그인버튼/이름) 매칭
                Session["MemberID"] = loginQuery.MemberID;
                Session["MemberName"] = loginQuery.MemberName;
                Session["MemberEmail"] = loginQuery.MemberEmail;
                Session["MemberPhone"] = loginQuery.MemberPhone;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return RedirectToAction("CommonError", "Home");
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
                return RedirectToAction("CommonError", "Home");
            }
        }

        // 회원 탈퇴
        [ActionName("MemberDrop")]
        public ActionResult MemberDrop()
        {
            try
            {
                // 세션에 값이 없을 경우 에러페이지로 이동
                if (Session["MemberID"] == null)
                {
                    throw new Exception();
                }
                bool queryResult = deleteMethod.MemberDrop(Convert.ToInt32(Session["MemberID"]));
                // 로그인이 실패할 경우 에러페이지로 이동
                if (queryResult == false)
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

        // 비밀번호 변경
        [HttpPost]
        [ActionName("ChangePassword")]
        public ActionResult ChangePassword()
        {
            try
            {
                // 비밀번호 사용자 입력값
                string oldPassword = Request.Form["oldPassword"];
                string newPassword = Request.Form["newPassword"];

                if (oldPassword.Equals(newPassword))
                {
                    // 비밀번호가 같으면 안됨.
                }
                bool passwordCheck = check.CheckPasswordLength(newPassword);
                if (!passwordCheck)
                {
                    // 비밀번호 규칙에 안맞으면 에러
                }
                bool changeResult = updateMethod.ChangePassword(oldPassword, newPassword);
                if (!changeResult)
                {
                    // 비밀번호 변경 실패
                }
                // 비밀번호변경으로 인해 재로그인해주세요 라는 메시지를 출력한 후 홈화면으로 간다.
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return RedirectToAction("CommonError", "Home");
            }
        }
    }
}