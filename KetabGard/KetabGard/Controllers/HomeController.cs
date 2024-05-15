using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KetabGard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard(string id)
        {
            int.TryParse(id, out int x);
            if (x == 1)
            {
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Userpage(string id)
        {
            int.TryParse(id, out int x);
            if (x == 1)
            {
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Bookpage(string id)
        {
            int.TryParse(id, out int x);
            if (x == 1)
            {
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Amanatpage(string id)
        {
            int.TryParse(id, out int x);
            if (x == 1)
            {
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Addadminpage(string id)
        {
            int.TryParse(id, out int x);
            if (x == 1)
            {
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult UsersTypeTable(int id)
        {
            return PartialView();
        }
        public ActionResult Adduserpage(string id)
        {
            int.TryParse(id, out int x);
            if (x == 1)
            {
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}