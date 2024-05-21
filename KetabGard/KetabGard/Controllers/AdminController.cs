using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KetabGard.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login(List<Userpass> userpass)
        {
            if (userpass[0].Username.ToLower() == "ali" && userpass[0].Password == "12345")
            {
                Session["Admin"] = userpass[0];
                return RedirectToAction("Index","Home");
            }
            else
            {
                return Json("error");
            }
        }
    }
}