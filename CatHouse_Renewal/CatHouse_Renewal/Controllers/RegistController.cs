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
        public ActionResult MemeberCreate(FormCollection memberForm)
        {
            // 비밀번호 암호화
            try
            {
                string memName = Request.Form["memName"];
                string memEmail = Request.Form["memEmail"];
                string memPassword = Request.Form["memPassword"];
                int memPhone = Convert.ToInt32(Request.Form["memPhone"]);
                string memAddress = Request.Form["memAddress"];

                // 실제로 체크
                bool emailCheck = check.CheckEmail(memEmail);
                bool passCheck = check.CheckPasswordLength(memPassword);

                // 이메일형식 또는 패스워드 길이가 맞지 않으면
                if (!emailCheck || !passCheck)
                {
                    throw new Exception();
                }

                // 새로운 멤버 객체 생성
                MemberModel newMem = new MemberModel();
                newMem.memName = memName;
                newMem.memEmail = memEmail;
                newMem.memPassword = memPassword;
                newMem.memPhone = memPhone;
                newMem.memAddress = memAddress;

                bool queryResult = insertDB.MemberInsert(newMem);
                // 아이템 추가
                // 결과에 따라서 환영페이지로 이동할지, 오류 메시지 출력 후 홈페이지로 이동할 지 결정
                if (queryResult)
                {
                    return RedirectToAction("Registered", "Regist");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                JavaScriptResult callPopup = JavaScript("showPopup('잘못입력하셨어요', '잘못입력하셨습니다. 다시 입력해주세요.', 400, 200)");
                
                Response.Write("<script type='text/javascript' src='~/Contents/common.js'>showPopup('잘못 입력하셨어요.','잘못되었습니다. 다시 입력해주세요.',400,200);</script>");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ActionName("CatCreate")]
        public ActionResult CatCreate()
        {
            try
            {
                //고유아이디/이름/나이/성별/중성화상태/사진/상태메모
                string catName = Request.Form["catName"];
                string catAge = Request.Form["catAge"];
                string catGender = Request.Form["catGender"];
                string catSurgery = Request.Form["catSurgery"];
                string catMemo = Request.Form["catMemo"];
                string catURL = Request.Form["catURL"];

                CatModel newCat = new CatModel();
                newCat.catName = catName;
                newCat.catAge = Convert.ToInt32(catAge);
                newCat.catMemo = catMemo;
                //newCat.catPhotoURL = catURL;
                newCat.catPhotoURL = "일단NULL";
                // 수컷인지 암컷인지 판별
                if (catGender.Equals("male"))
                {
                    // 수컷이면
                    newCat.catGender = 1;
                }
                else
                {
                    // 암컷이면
                    newCat.catGender = 0;
                }
                // 중성화 여부 판별
                if (catSurgery.Equals("yes"))
                {
                    newCat.catNeuter = 1;
                }
                else
                {
                    newCat.catNeuter = 0;
                }

                // 최종 결과
                bool queryResult = insertDB.CatInsertToDB(newCat);
                if (queryResult)
                {
                    // 알람 띄워주고 
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        [ActionName("TraderCreate")]
        public void TraderCreate()
        {

        }
    }
}