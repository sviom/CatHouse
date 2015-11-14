﻿using CatHouse_Renewal.DB;
using CatHouse_Renewal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatHouse_Renewal.Controllers
{
    public class HomeController : Controller
    {
        // Insert 관련 함수들 모음
        InsertDB insertDB = new InsertDB();
        // 점검 관련 함수들 모음
        Check check = new Check();

        [ActionName("Index")]
        public ActionResult Index()
        {
            return View();
        }

        // 소개페이지로 이동
        //public ActionResult About()
        //{
        //    return View();
        //}

        // 로그인 화면으로 이동
        public ActionResult Login()
        {
            return View();
        }
    }
}