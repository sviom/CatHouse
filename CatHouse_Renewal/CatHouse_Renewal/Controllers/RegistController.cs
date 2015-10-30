using CatHouse_Renewal.Models;
using CatHouse_Renewal.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CatHouse_Renewal.Controllers
{
    public class RegistController : Controller
    {
        // Insert 관련 함수들 모음
        InsertDB insertDB = new InsertDB();
        // 점검 관련 함수들 모음
        Check check = new Check();

        // GET: Regist
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult CatRegist()
        {
            return View();
        }

        public ActionResult MainRegist()
        {
            return View();
        }

        public ActionResult MemberRegist()
        {
            return View();
        }

        public ActionResult TraderRegist()
        {
            return View();
        }


        [ActionName("Registered")]
        public ActionResult Registered()
        {
            return View();
        }

        [ActionName("MemberCreate")]
        public void MemeberCreate()
        {
            // 비밀번호 암호화
            try
            {
                //string memName = Request.Form["memName"];
                //string memEmail = Request.Form["memEmail"];
                //string memPassword = Request.Form["memPW"];
                //int memPhone = Convert.ToInt32(Request.Form["memPhone"]);
                //string memAddress = Request.Form["memAddress"];

                //// 실제로 체크
                //bool emailCheck = check.CheckEmail(memEmail);
                //bool passCheck = check.CheckPasswordLength(memPassword);

                //// 이메일형식 또는 패스워드 길이가 맞지 않으면
                //if (!emailCheck || !passCheck)
                //{
                //    throw new Exception();
                //}

                //// 새로운 멤버 객체 생성
                //MemberModel newMem = new MemberModel();
                //newMem.memName = memName;
                //newMem.memEmail = memEmail;
                //newMem.memPassword = memPassword;
                //newMem.memPhone = memPhone;
                //newMem.memAddress = memAddress;

                //insertDB.MemberInsert(newMem);
                // 아이템 추가
                // 결과에 따라서 환영페이지로 이동할지, 오류 메시지 출력 후 홈페이지로 이동할 지 결정
                //if (queryResult)
                //{
                //    //return RedirectToAction("MemberWelcome", "Member");
                //}
                //else
                //{
                //    throw new Exception();
                //}
                RedirectToAction("Registered", "Regist");
            }
            catch
            {
                //return RedirectToAction("Error", "Home");
            }
        }

        [ActionName("CatCreate")]
        public void CatCreate()
        {
        }

        [ActionName("TraderCreate")]
        public void TraderCreate()
        {

        }
    }
}