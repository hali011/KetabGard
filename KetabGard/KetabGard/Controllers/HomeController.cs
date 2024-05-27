using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KetabGard.Models;

namespace KetabGard.Controllers
{
    public class HomeController : Controller
    {
        #region Login
        public ActionResult Login()
        {
            return View();
        }
        #endregion

        #region Basepage
        public ActionResult Index()
        {
            var Sadmin = new Admin();
            Sadmin = Session["Admin"] as Admin;
            if (Sadmin != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        #endregion

        #region Menu
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
                List<int> data = GetGender();
                ViewBag.man = data[0];
                ViewBag.woman = data[1];
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
                List<Genre> genre = new List<Genre>();
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    genre = db.Genres.ToList();
                }
                ViewBag.bgenre = genre;
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
        #endregion

        #region UserPageSection
        public ActionResult UsersTypeTable(int id)
        {
            var users = new List<User>();
            using (KetabGardEntities db = new KetabGardEntities())
            {
                users = db.Users.ToList();
            }
            return PartialView(users);
        }
        public ActionResult Adduserpage(string id)
        {
            int.TryParse(id, out int x);
            if (x == 1)
            {
                ViewBag.gendre = new List<Gender>();
                ViewBag.education = new List<EducationDegree>();
                User us =  new User();
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    ViewBag.gendre = db.Genders.ToList();
                    ViewBag.education = db.EducationDegrees.ToList();
                }
                return PartialView(us);
            }
            else if (x > 1)
            {
                var user = new User();
                ViewBag.gendre = new List<Gender>();
                ViewBag.education = new List<EducationDegree>();
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    ViewBag.gendre = db.Genders.ToList();
                    ViewBag.education = db.EducationDegrees.ToList();
                    user = db.Users.Where(w => w.Id == x).SingleOrDefault();
                }
                return PartialView(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public JsonResult Saveuser(int edit , List<User> users)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            DateTime now = DateTime.Now;

            bool Confirm = false;
            var user = users[0];

            if (edit == 0)
            {
                int persianYear = persianCalendar.GetYear(now);
                int persianMonth = persianCalendar.GetMonth(now);
                int persianDay = persianCalendar.GetDayOfMonth(now);

                user.SaveDateD = persianDay;
                user.SaveDateM = persianMonth;
                user.SaveDateY = persianYear;
                user.CountOfGet = 0;
                user.Active = true;
                user.Expireactivate = 30;
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    Confirm = true;
                }
                if (Confirm)
                {
                    return Json(new { confirm = true });
                }
                else
                {
                    return Json(new { confirm = false });
                }
            }
            else
            {
                user.Id = edit;
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    var duser = db.Users.Where(w=>w.Id == user.Id).SingleOrDefault();
                    duser.FirstName = user.FirstName; duser.LastName = user.LastName; duser.Email = user.Email; duser.PhoneNumber = user.PhoneNumber; duser.FatherName = user.FatherName; duser.MdliCode = user.MdliCode; duser.EducationDegree = user.EducationDegree; duser.Gender = user.Gender;
                    db.Entry(duser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    Confirm = true;
                }
                if (Confirm)
                {
                    return Json(new { confirm = true });
                }
                else
                {
                    return Json(new { confirm = false });
                }
            }
            
        }
        public JsonResult deleteuser(int id)
        {
            bool Confirm = false;
            var user = new User();
            using (KetabGardEntities db = new KetabGardEntities())
            {
                user = db.Users.Where(w => w.Id == id).SingleOrDefault();
                db.Users.Remove(user);
                db.SaveChanges();
                Confirm = true;
            }
             return Json(new { confirm = Confirm });
        }
        #endregion

        #region loaderpage
        public ActionResult loader()
        {
            return PartialView();
        }
        #endregion

        #region BookPageSection
        public ActionResult BookTable()
        {
            var books = new List<Book>();
            List<Genre> genre = new List<Genre>();

            using (KetabGardEntities db = new KetabGardEntities())
            {
                genre = db.Genres.ToList();
                books = db.Books.ToList();
            }
            ViewBag.bgenre = genre;
            return PartialView(books);
        }
        public JsonResult Delbook(int id)
        {
            bool flag = false;
            using (KetabGardEntities db = new KetabGardEntities())
            {
                var book = db.Books.Where(w => w.Id == id).SingleOrDefault();
                db.Books.Remove(book);
                db.SaveChanges();
                flag = true;
            }
            return Json(new { flag = flag });
        }
        public ActionResult Addbookpage(string id)
        {
            int.TryParse(id, out int x);
            if (x == 1)
            {
                List<Genre> gn = new List<Genre>();
                var book = new Book();

                using (KetabGardEntities db = new KetabGardEntities())
                {
                    gn = db.Genres.ToList();
                }
                ViewBag.genre = gn;
                return PartialView(book);
            }
            else if (x > 1)
            {
                List<Genre> gn = new List<Genre>();
                var book = new Book();

                using (KetabGardEntities db = new KetabGardEntities())
                {
                    book = db.Books.Where(w=>w.Id == x).SingleOrDefault();
                    gn = db.Genres.ToList();
                }
                ViewBag.genre = gn;
                return PartialView(book);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public JsonResult Savenewbook(Book book)
        {
            if (book.Id == 0)
            {
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    book.GetCount = 0;
                    db.Books.Add(book);
                    db.SaveChanges();
                }
                return Json(new { sucess = "عملیات با موفقیت انجام شد" });
            }
            else
            {
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    var editbook = db.Books.Where(w => w.Id == book.Id).SingleOrDefault();
                    editbook.Name = book.Name;editbook.Writer = book.Writer;editbook.PicURL = book.PicURL;editbook.Genre = book.Genre;editbook.YearofPublication = book.YearofPublication; editbook.Pages = book.Pages;
                    db.Entry(editbook).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Json(new { sucess = "عملیات با موفقیت انجام شد" });
            }
            
        }

        public JsonResult Savegenrebook(string genre)
        {
            bool flag = false;
            Genre gn = new Genre();
            gn.Name = genre;

            using (KetabGardEntities db = new KetabGardEntities())
            {
                if (db.Genres.Where(w=>w.Name == gn.Name).SingleOrDefault() == null)
                {
                    flag = true;

                    db.Genres.Add(gn);
                    db.SaveChanges();
                    gn = db.Genres.Where(w => w.Name == gn.Name).SingleOrDefault();
                }
            }
            return Json(new {flag = flag , id = gn.Id});
        }
        #endregion

        #region AdminPageSection
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
        #endregion

        #region tools
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

        public List<int> GetGender()
        {
            var data = new List<int>();
            var mans = new List<User>();
            var womans = new List<User>();

            using (KetabGardEntities db = new KetabGardEntities())
            {
                mans = db.Users.Where(w => w.Gender == 1).ToList();
                womans = db.Users.Where(w => w.Gender == 2).ToList();
            }

            int countofmans = mans.Count;
            int countofwomans = womans.Count;

            int totals = countofmans + countofwomans;
            data.Add((countofmans * 100) / totals);
            data.Add((countofwomans * 100) / totals);

            return data;
        }
        #endregion

        #region AmanatiPageSection
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
        #endregion





    }
}