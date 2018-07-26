using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwjDataBaseManage.Service;

namespace EricDataBaseManage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Table()
        {
            return View();
        }

        public JsonResult GetTableInfo(string d, string t)
        {
            return Json(ServiceCode.ReadTableInfo(d, t), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ReadTableList(string d, string t)
        {
            return Json(ServiceCode.GetTableList(), JsonRequestBehavior.AllowGet);
        }
    }
}
