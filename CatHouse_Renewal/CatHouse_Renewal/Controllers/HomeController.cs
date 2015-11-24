using CatHouse_Renewal.DB;
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
        [ActionName("Index")]
        public ActionResult Index()
        {
            return View();
        }

        // 에러페이지로 이동
        public ActionResult CommonError()
        {
            return View();
        }
    }
}