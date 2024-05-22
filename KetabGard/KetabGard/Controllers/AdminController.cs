using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KetabGard.Models;

namespace KetabGard.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Login(List<Admin> userpass)
        {
            var sAdmin = new Admin();
            sAdmin = Session["Admin"] as Admin;
            if (sAdmin != null)
            {
                return Json(new { admin = true});
            }
            else
            {
                var admins = new List<Admin>();
                Admin admin = new Admin();

                using (KetabGardEntities db = new KetabGardEntities())
                {
                    admins = db.Admins.ToList();
                }

                foreach (var item in admins)
                {
                    if (userpass[0].Username.ToLower() == item.Username.ToLower())
                    {
                        admin = item;
                    }
                }

                if (userpass[0].Username.ToLower() == admin.Username.ToLower() && userpass[0].Password == admin.Password)
                {
                    Session["Admin"] = admin;
                    return Json(new {admin=false});
                }
                else
                {
                    return Json(new {error = true});
                }
            }
        }
    }
}