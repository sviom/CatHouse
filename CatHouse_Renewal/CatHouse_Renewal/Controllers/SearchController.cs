﻿using CatHouse_Renewal.DB;
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

        public ActionResult MainFinder()
        {
            return View();
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
            catch(Exception ex)
            {
                ex.Message.ToString();
                // 값이 없거나 에러이면 Null을 넘긴다.
                ViewBag.lng = null;
                ViewBag.lnt = null;
                return View();
            }
        }


        public ActionResult FilterFinder()
        {
            return View();
        }


        [HttpPost]
        [ActionName("NearTraderFinder")]
        // 지도 찾기에서 해당 회원 근처 5곳의 장소를 얻어와 지도에 마커로 표시한다.
        public void NearTraderFinder()
        {
            try
            {
                // 해당 회원의 좌표를 얻어온다.
                // DB로 부터 MemID를 넘겨 해당 회원의 좌표값을 알아온다.
                HomeModel memAddress = selectDB.MemberCoordinateSelect(Convert.ToInt32(Session["MemberID"]));

                // 해당 좌표를 주소로 변환해 주소 값을 얻어온다.

                // 주소 값으로 지도에서 검색한다.

                // 검색한 키워드의 근처 5곳의 좌표를 얻어온다.

                // 좌표값을 배열로 만들어 View로 넘긴다.
            }
            catch(Exception ex)
            {

            }
        }
    }
}