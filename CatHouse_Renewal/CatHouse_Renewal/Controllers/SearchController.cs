using CatHouse_Renewal.DB;
using CatHouse_Renewal.Models;
using CatHouse_Renewal.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatHouse_Renewal.Controllers
{
    public class SearchController : Controller
    {
        // 로그인 및 Member 관련 DB Connection모음
        LoginConnection loginConn = new LoginConnection();
        // Insert 관련 함수들 모음
        InsertDB insertDB = new InsertDB();
        // Select 관련 함수들 모음
        SelectDB selectDB = new SelectDB();
        // 점검 관련 함수들 모음
        Check check = new Check();

        // 찾기 기본화면으로 이동
        public ActionResult MainFinder()
        {
            return View();
        }

        // 조건 검색 화면으로 이동
        public ActionResult FilterFinder()
        {
            return View();
        }


        // 조건 검색 결과 화면으로 이동
        [HttpPost,ActionName("FilterSearchResult")]
        public ActionResult FilterSearchResult()//string existPet, int petNum
        {
            try
            {
                // 여러가지 조건이 있지만 일단 기존 동물 여부와 마릿수에 대한 것만 검색해서 넘겨준다.
                string existPet1 = Request.Form["existingYN"].ToString();
                int amount1 = Convert.ToInt32(Request.Form["amount"]);

                // 업자의 정보를 전부 가져온다. --> FilterSearchView라는 모델로 받아온다.
                List<FilterSearchView> traderlist = selectDB.TraderSelectWithFilter(existPet1, amount1);
                if(traderlist == null)
                {
                    throw new Exception();
                }
                // 해당 업자의 정보 중에서 
                return View(traderlist);
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return View();
            }

        }

        // 지도 찾기로 이동
        public ActionResult MapFinder()
        {
            // 해당 항목으로 이동 할 때 DB에서 해당 회원의 주소를 검색 한 다음 지도를 보내서 기준을 설정
            try
            {
                // DB로 부터 MemID를 넘겨 해당 회원의 좌표값을 알아온다.
                HomeModel memAddress = selectDB.MemberCoordinateSelect(Convert.ToInt32(Session["MemberID"]));
                // 해당 항목을 ','로 쪼개서 좌표값으로 만든다.
                string[] memCoordinate = memAddress.coordinate.Split(',');
                ViewBag.lng = memCoordinate[0];
                ViewBag.lnt = memCoordinate[1];
                // View로 넘긴다.
                return View();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                // 값이 없거나 에러이면 Null을 넘긴다.
                ViewBag.lng = null;
                ViewBag.lnt = null;
                return View();
            }
        }

        /// <summary>
        /// 필터로 DB에서 조건에 맞는 사람을 찾는다.
        /// </summary>
        [HttpPost]
        [ActionName("FindTraderWithFilter")]
        public ActionResult FindTraderWithFilter()
        {
            try
            {
                // 여러가지 조건이 있지만 일단 기존 동물 여부와 마릿수에 대한 것만 검색해서 넘겨준다.
                string existPet = Request.Form["existingYN"].ToString();
                int amount = Convert.ToInt32(Request.Form["amount"]);

                // 리스트를 검색 결과 화면으로 전송한다.
                return RedirectToAction("FilterSearchResult", new { existPet = existPet, petNum = amount });
                //FilterSearchResult(existPet, amount);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
    }
}