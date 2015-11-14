using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatHouse_Renewal.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult MapFinder()
        {
            return View();
        }

        public ActionResult FilterFinder()
        {
            return View();
        }
    }
}