using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CatHouse_Renewal.Controllers;
using CatHouse_Renewal.DB;
using CatHouse_Renewal.Models;
using CatHouse_Renewal.Other;
using Newtonsoft.Json;

//using Microsoft.Framework.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using Microsoft.Azure;
using System.Configuration;
//using LogLevel = Microsoft.Framework.Logging.LogLevel;

namespace CatHouse_Renewal.Controllers
{
    public class RegistController : Controller
    {
        // 로그인 및 Member 관련 DB Connection모음
        LoginConnection loginConn = new LoginConnection();
        // Insert 관련 함수들 모음
        InsertDB insertDB = new InsertDB();
        // SELECT 관련 함수들 모음
        SelectDB selectMethod = new SelectDB();
        // 점검 관련 함수들 모음
        Check check = new Check();

        // 고양이 등록 페이지로 이동
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

        // 메인 등록 페이지로 이동
        public ActionResult MainRegist()
        {
            return View();
        }

        // 회원 등록 페이지로 이동
        public ActionResult MemberRegist()
        {
            try
            {
                // 로그인이 되어 있으면 회원가입을 할 수 없다.(세션에 들어있는 ID값 이용)
                if (Convert.ToInt32(Session["MemberID"]) > 0)
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

        // 업체 회원 등록페이지로 이동
        public ActionResult TraderRegist()
        {
            try
            {
                // 로그인이 되어 있으면 정상적으로 등록페이지에 들어갈 수 있다.(세션에 들어있는 ID값 이용)
                if (Convert.ToInt32(Session["MemberID"]) <= 0)
                {
                    throw new Exception();
                }
                // 또는 이미 업자로 등록되어 있으면 등록되어 있다로 해야함
                if (selectMethod.CheckTrader(Convert.ToInt32(Session["MemberID"])))
                {
                    // 여기에 메시지를 담아서 View로 던져서 View에서 Alert를 띄울 수 있도록 해야함
                    return View();
                }
                return View();
            }
            catch (Exception ex)
            {
                // 안되어 있으면 홈으로 되돌아간다.
                return RedirectToAction("Index", "Home");
            }
        }

        // 등록 성공 후 환영페이지로 이동
        [HttpPost]
        [ActionName("Registered")]
        public ActionResult Registered()
        {
            return View();
        }

        // 회원 등록
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
                //string memAddress = Request.Form["memAddress"];
                // 집 주소 저장 
                HomeModel homeAddress = new HomeModel();
                homeAddress.postCode = Request.Form["postCode"].ToString();     // 우편번호
                homeAddress.roadAddress = Request.Form["roadAddress"].ToString();   // 도로명주소
                homeAddress.mapAddress = Request.Form["mapAddress"].ToString();     // 지번 주소
                homeAddress.coordinate = Request.Form["coordinate"].ToString();     // 좌표
                //coordinate
                string memAddress = JsonConvert.SerializeObject(homeAddress);

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
                    // 세션에 MEM ID생성한 값을 저장한다.
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

        // 고양이 등록
        [HttpPost]
        [ActionName("CatCreate")]
        public ActionResult CatCreate()
        {
            try
            {
                // 고유아이디/이름/나이/성별/중성화상태/사진/상태메모
                string catName = Request.Form["catName"];
                string catAge = Request.Form["catAge"];
                string catGender = Request.Form["catGender"];
                string catSurgery = Request.Form["catSurgery"];
                string catMemo = Request.Form["catMemo"];
                string catURL = Request.Form["catURL"];

                // 새로운 고양이 생성
                CatModel newCat = new CatModel();
                newCat.catName = catName;
                newCat.catAge = Convert.ToInt32(catAge);
                newCat.catMemo = catMemo;
                //newCat.catPhotoURL = catURL;
                newCat.catPhotoURL = "NotPhotoURL";

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
                // FK로 연동되는 MemberID
                newCat.memID = Convert.ToInt32(Session["MemberID"]);

                // 최종 결과
                bool queryResult = insertDB.CatInsertToDB(newCat);
                // 결과에 에러가 있으면 오류페이지로 이동
                if (queryResult == false)
                {
                    throw new Exception();
                }
                // 알람 띄워주고 
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return RedirectToAction("CommonError", "Home");
            }
        }

        // 업자 회원 등록
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
                
                // 기존 동물 여부 판단
                if (existPet.Equals("yes"))
                {
                    // 수컷이면
                    newTraderMem.existPet = true;
                }
                else
                {
                    // 암컷이면
                    newTraderMem.existPet = false;
                }

                newTraderMem.existPetIntro = Request.Form["existPetNumber"].ToString();
                newTraderMem.homeAddress = "구로구항동";
                newTraderMem.homePhotoURL = "어딘가에 있을사진";
                // FK로 연동되는 MemberID
                newTraderMem.memID = Convert.ToInt32(Session["MemberID"]);

                // 아이템 추가
                bool queryResult = insertDB.TraderMemberInsertToDB(newTraderMem);

                if (queryResult == false)
                {
                    throw new Exception();
                }
                // 환영 showPopup을 띄운 후 이동
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // 에러 팝업을 띄운 후 그대로 있는다.
                ex.Message.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        // 
    }
}