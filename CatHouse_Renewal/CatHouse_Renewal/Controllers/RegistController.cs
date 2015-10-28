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

        [ActionName("MemberCreate")]
        public void MemeberCreate()
        {
            // 비밀번호 암호화

        }
    }
}