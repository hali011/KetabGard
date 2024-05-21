using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KetabGard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login(string user , string pass)
        {
            return View();
        }
        public ActionResult Index()
        {
            var us = Session["Admin"] as Userpass; 
            if (us != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
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
        public ActionResult Adminpage(string id)
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
        public ActionResult Addbookpage(string id)
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
        [HttpPost]
        public JsonResult UploadImage()
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    int x = Request.Files.Count;
                    string uploadDir = "";
                    switch (x)
                    {
                        case 1:
                            uploadDir = Server.MapPath("~/AdminPic/");
                            break;
                        case 2:
                            uploadDir = Server.MapPath("~/BookPic/");
                            break;
                        case 3:
                            uploadDir = Server.MapPath("~/UserPic/");
                            break;
                    }
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    var filePath = Path.Combine(uploadDir, file.FileName);
                    file.SaveAs(filePath);

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "No file uploaded" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public ActionResult AmanatiTypeTable(int id)
        {
            return PartialView();
        }
        public ActionResult Addamanatipage(string id)
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
        public ActionResult booksearch(string bookname)
        {
            ViewBag.serachof = "کتاب";
            return PartialView("Searchresult");
        }
        public ActionResult usersearch(string nameorcdm)
        {
            ViewBag.serachof = "عضو";
            return PartialView("Searchresult");
        }
    }
}