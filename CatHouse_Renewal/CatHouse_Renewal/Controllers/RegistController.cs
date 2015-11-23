using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using CatHouse_Renewal.Controllers;
using CatHouse_Renewal.DB;
using CatHouse_Renewal.Models;

namespace CatHouse_Renewal.Controllers
{
    public class RegistController : Controller
    {
        // Insert 관련 함수들 모음
        InsertDB insertDB = new InsertDB();
        // 점검 관련 함수들 모음
        Check check = new Check();
        public ActionResult CatRegist()
        {
            try
            {
                // 로그인이 되어 있으면 정상적으로 등록페이지에 들어갈 수 있다.(세션에 들어있는 ID값 이용)
                if (Convert.ToInt32(Session["MemberID"]) <= 0)
                {
                    throw new Exception();
                }
                return View();
            }
            catch (Exception ex)
            {
                // 안되어 있으면 홈으로 되돌아간다.
                return RedirectToAction("Index", "Home");
            }
        }

        // 메인 레지스트 페이지 접속
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

        [HttpPost]
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

                // 이메일/패스워드 길이 검사
                bool emailCheck = check.CheckEmail(memEmail);
                bool passCheck = check.CheckPasswordLength(memPassword);

                // 이메일형식 또는 패스워드 길이가 맞지 않으면
                if (!emailCheck || !passCheck)
                {
                    // 에러
                    throw new Exception();
                }

                // 새로운 멤버 객체 생성
                MemberModel newMem = new MemberModel();
                newMem.memName = memName;
                newMem.memEmail = memEmail;
                newMem.memPassword = memPassword;
                newMem.memPhone = memPhone;
                newMem.memAddress = memAddress;

                // 실제로 DB에 넣는다. 실행 후 성공 여부를 Return 한다.
                bool queryResult = insertDB.MemberInsert(newMem);

                // 결과에 따라서 환영페이지로 이동할지, 오류 메시지 출력 후 홈페이지로 이동할 지 결정
                if (queryResult)
                {
                    // 실제로 DB에 들어갔으면 환영페이지 생성
                    return RedirectToAction("Registered", "Regist");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                ex.Message.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ActionName("TraderCreate")]
        public ActionResult TraderCreate()
        {
            try
            {
                string homeIntro = Request.Form["homeIntro"];
                int homePrice = Convert.ToInt32(Request.Form["homePrice"]);
                string homeGoods = Request.Form["homeGoods"];
                string existPet = Request.Form["existPet"];

                // 새로운 멤버 객체 생성
                TraderModel newTraderMem = new TraderModel();
                newTraderMem.homePrice = homePrice;
                newTraderMem.homeIntro = homeIntro;
                newTraderMem.existPet = true;
                newTraderMem.existPetIntro = "기존에 있는 동물 소개";
                newTraderMem.homeAddress = "구로구항동";
                newTraderMem.homePhotoURL = "어딘가에 있을사진";

                // 아이템 추가
                bool queryResult = insertDB.TraderMemberInsertToDB(newTraderMem);

                if (queryResult)
                {
                    // 환영 showPopup을 띄운 후 이동
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                // 에러 팝업을 띄운 후 그대로 있는다.
                ex.Message.ToString();
                return RedirectToAction("Error", "Home");
            }
        }
    }
}