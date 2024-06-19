using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using Jdf;
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
                ViewBag.pic = Sadmin.Picurl;
                ViewBag.name = Sadmin.Name + " " + Sadmin.Lastname;
                ViewBag.id = Sadmin.id;
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
                double weakavrage = Math.Round(Getweakavrage(),1);
                int userscount = Getuserscount();
                int amanatcount = Getamanatcount();
                int amanatnotbackcount = Getamanatnotbackcount();
                int[] thisweak = Getdatathisweak();
                int[] prweak = Getprweak();
                ViewBag.avrage = weakavrage;
                ViewBag.userscount = userscount;
                ViewBag.amanatcount = amanatcount;
                ViewBag.amanatnotbackcount = amanatnotbackcount;
                ViewBag.thisweak = thisweak[0] + " " + thisweak[1] + " " + thisweak[2] + " " + thisweak[3] + " " + thisweak[4] + " " + thisweak[5] + " " + thisweak[6];
                ViewBag.prweak = prweak[0] + " " + prweak[1] + " " + prweak[2] + " " + prweak[3] + " " + prweak[4] + " " + prweak[5] + " " + prweak[6];

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
                List<Book> books = new List<Book>();
                List<Genre> genre = new List<Genre>();
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    genre = db.Genres.ToList();
                    books = db.Books.OrderByDescending(o=>o.GetCount).Take(5).ToList();
                }
                ViewBag.bgenre = genre;
                ViewBag.book = books;
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
                List<User> bestuser = new List<User>();
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    bestuser = db.Users.OrderByDescending(o => o.CountOfGet).Take(5).ToList();
                }
                ViewBag.bestuser = bestuser;
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

        #region loaderpage
        public ActionResult loader()
        {
            return PartialView();
        }
        #endregion

        #region DashboardPageSection
        public ActionResult Dashboardtable()
        {
            List<DashboardTable> dbtbl = new List<DashboardTable>();
            using (KetabGardEntities db = new KetabGardEntities())
            {
                dbtbl = db.DashboardTables.OrderByDescending(o => o.CountOfGet).ToList();
            }
            return PartialView(dbtbl);
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
            switch (id)
            {
                case 0:
                    users = users.OrderBy(o => o.Id).ToList();
                    break;
                case 1:
                    users = users.OrderByDescending(o => o.CountOfGet).ToList();
                    break;
                case 2:
                    users = users.OrderBy(o => o.CountOfGet).ToList();
                    break;
            }
            var sAdmin = Session["Admin"] as Admin;
            ViewBag.adminaccess = sAdmin.Access;
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
                    duser.FirstName = user.FirstName; duser.LastName = user.LastName; duser.Email = user.Email; duser.PhoneNumber = user.PhoneNumber; duser.FatherName = user.FatherName; duser.MdliCode = user.MdliCode; duser.EducationDegree = user.EducationDegree; duser.Gender = user.Gender; duser.PicURL = user.PicURL;
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
            var sAdmin = Session["Admin"] as Admin;
            ViewBag.adminaccess = sAdmin.Access;
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
                    book.Here = true;
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

        #region AmanatiPageSection
        public ActionResult AmanatiTypeTable(int id)
        {
            List<Amanattable> amanat = new List<Amanattable>();
            switch (id)
            {
                case 0:
                    
                    using (KetabGardEntities db = new KetabGardEntities())
                    {
                        amanat = db.Amanattables.OrderBy(o=>o.Id).ToList();
                    }
                break;
                case 1:
                    using (KetabGardEntities db = new KetabGardEntities())
                    {
                        amanat = db.Amanattables.OrderByDescending(o => o.Active == true).ToList();
                    }
                break;
                case 2:
                    using (KetabGardEntities db = new KetabGardEntities())
                    {
                        amanat = db.Amanattables.OrderBy(o => o.Backed == null).ToList();
                    }
                break;
            }
            var sAdmin = Session["Admin"] as Admin;
            ViewBag.adminaccess = sAdmin.Access;
            return PartialView(amanat);
        }
        public ActionResult Addamanatipage(string id)
        {
            Amanattable amanat = new Amanattable();
            User user = new User();
            Book book = new Book();
            int.TryParse(id, out int x);
            if (x == 1)
            {
                return PartialView();
            }
            else if (x > 1)
            {
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    amanat = db.Amanattables.Where(w => w.Id == x).SingleOrDefault();
                    user = db.Users.Where(w => w.Id == amanat.Userid).SingleOrDefault();
                    book = db.Books.Where(w => w.Id == amanat.Bookid).SingleOrDefault();
                }
                ViewBag.amanat = amanat;
                ViewBag.book = book;
                ViewBag.user = user;
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public JsonResult Savenewamanat(Amanat amanat)
        {
            bool flag = false;
            if (amanat.Id == 0)
            {
                User user = new User();
                Book book = new Book();
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    user = db.Users.Where(w => w.Id == amanat.Userid).SingleOrDefault();
                    book = db.Books.Where(w => w.Id == amanat.Bookid).SingleOrDefault();

                    book.Here = false;
                    book.GetCount++;
                    book.WhoIsGet = user.Id;
                    book.WhenBack = amanat.Expiredate;
                    book.Backed = false;

                    user.CountOfGet++;
                    user.BookId = book.Id;
                    user.LastBook = book.Id;
                    user.WhenGiveBook = amanat.Expiredate;

                    amanat.Backed = null;

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                    db.Amanats.Add(amanat);
                    db.SaveChanges();
                    flag = true;
                }
            }
            else
            {
                Amanat amanati = new Amanat();
                User user = new User();
                Book book = new Book();
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    amanati = db.Amanats.Where(w => w.Id == amanat.Id).SingleOrDefault();
                    book = db.Books.Where(w => w.Id == amanati.Bookid).SingleOrDefault();
                    user = db.Users.Where(w => w.Id == amanati.Userid).SingleOrDefault();

                    book.Here = true;
                    book.WhenBack = null;
                    book.Backed = true;

                    user.CountOfGet --;

                    book = db.Books.Where(w => w.Id == amanat.Bookid).SingleOrDefault();
                    user = db.Users.Where(w => w.Id == amanat.Userid).SingleOrDefault();

                    book.Here = false;
                    book.GetCount++;
                    book.WhoIsGet = user.Id;
                    book.WhenBack = amanat.Expiredate;
                    book.Backed = false;

                    user.CountOfGet++;
                    user.BookId = book.Id;
                    user.LastBook = book.Id;
                    user.WhenGiveBook = amanat.Expiredate;

                    amanati.Name = amanat.Name;
                    amanati.Userid = amanat.Userid;
                    amanati.Bookid = amanat.Bookid;

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(amanati).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    flag = true;
                }
            }
            return Json(new {success = flag});
        }
        public JsonResult BackedAmanat(int id)
        {
            bool flag = false;
            Amanat amanat = new Amanat();
            User user = new User();
            Book book = new Book();

            using (KetabGardEntities db = new KetabGardEntities())
            {
                amanat = db.Amanats.Where(w => w.Id == id).SingleOrDefault();
                user = db.Users.Where(w=>w.Id == amanat.Userid).SingleOrDefault();
                book = db.Books.Where(w => w.Id == amanat.Bookid).SingleOrDefault();

                amanat.Active = false;
                amanat.Expireday = 0;
                amanat.Backed = true;

                user.BookId = 0;
                
                book.Here = true;
                book.WhoIsGet = 0;
                book.Backed = true;

                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                db.Entry(amanat).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                flag = true;
            }
            return Json(new {success = flag});
        }
        #endregion

        #region AdminPageSection
        public ActionResult Addadminpage(string id)
        {
            int.TryParse(id, out int x);
            if (x == 1)
            {
                var access = new List<Access>();
                var education = new List<EducationDegree>();
                var admin = new Admin();
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    access = db.Accesses.ToList();
                    education = db.EducationDegrees.ToList();
                }
                ViewBag.education = education;
                ViewBag.access = access;
                return PartialView(admin);
            }
            else if (x > 1)
            {
                var access = new List<Access>();
                var education = new List<EducationDegree>();
                var admin = new Admin();
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    access = db.Accesses.ToList();
                    education = db.EducationDegrees.ToList();
                    admin = db.Admins.Where(w=>w.id == x).SingleOrDefault();
                }
                ViewBag.education = education;
                ViewBag.access = access;
                return PartialView(admin);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult AdminTable()
        {
            var admins = new List<Admin>();
            var access = new List<Access>();
            using (KetabGardEntities db = new KetabGardEntities())
            {
                admins = db.Admins.ToList();
                access = db.Accesses.ToList();
            }
            var sAdmin = Session["Admin"] as Admin;
            ViewBag.adminaccess = sAdmin.Access;
            ViewBag.accesses = access;
            return PartialView(admins);
        }
        public JsonResult DelAdmin(int id)
        {
            var admin = new Admin();

            using (KetabGardEntities db = new KetabGardEntities())
            {
                admin = db.Admins.Where(w => w.id == id).SingleOrDefault();
                db.Admins.Remove(admin);
                db.SaveChanges();
            }
            return Json(new {success = "عملیات با موفقیت انجام شد"});
        }

        public JsonResult AddnewAccess(string name)
        {
            bool flag = false;
            var access = new Access();
            var newaccess = new Access();
            using (KetabGardEntities db = new KetabGardEntities())
            {
                access = db.Accesses.Where(w=>w.Name == name).SingleOrDefault();
            }
            if (access == null)
            {
                
                newaccess.Name = name;
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    db.Accesses.Add(newaccess);
                    db.SaveChanges();
                    flag = true;
                }
            }
            return Json(new {success = flag, id = newaccess.Id});
        }

        public JsonResult Savenewadmin(Admin admin)
        {
            if (admin.id == 0)
            {
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    db.Admins.Add(admin);
                    db.SaveChanges();
                }
                return Json(new { success = true });
            }
            else
            {
                var editadmin = new Admin();
                editadmin.id = admin.id; editadmin.Name = admin.Name; editadmin.Lastname = admin.Lastname; editadmin.Access = admin.Access; editadmin.Mcode = admin.Mcode; editadmin.Picurl = admin.Picurl; editadmin.Username = admin.Username; editadmin.Password = admin.Password; editadmin.Phonenumber = admin.Phonenumber; editadmin.Email = admin.Email; editadmin.EducationDegree = admin.EducationDegree;
                using (KetabGardEntities db = new KetabGardEntities())
                {
                    db.Entry(editadmin).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Json(new { success = true });
            }
            
        }
        #endregion

        #region tools
        private int[] Getprweak()
        {
            int[] result = new int[7];
            List<User> user = new List<User>();
            using (KetabGardEntities db = new KetabGardEntities())
            {
                user = db.Users.ToList();
            }
            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            int[] all = DateConverter.gregorian_to_jalali(year, month, day);

            year = all[0];
            month = all[1];
            day = all[2];

            int[] sevendayago = new int[3];

            for (int i = 1; i <= 2; i++)
            {
                sevendayago = Getsevendaysago(year, month, day);
                if (i == 1)
                {
                    year = sevendayago[0];
                    month = sevendayago[1];
                    day = sevendayago[2];
                }
            }

            if (year == sevendayago[0])
            {
                if (month == sevendayago[1])
                {
                    int d = 0;
                    for (int i = sevendayago[2]; i <= day; i++)
                    {
                        using (KetabGardEntities db = new KetabGardEntities())
                        {
                            user = db.Users.Where(w => w.SaveDateD == i).ToList();
                        }
                        result[d] = user.Count;
                        d++;
                    }
                }
                else
                {
                    int d = 0;
                    for (int i = sevendayago[1]; i <= month; i++)
                    {
                        if (i < month)
                        {
                            for (int v = sevendayago[2]; v <= 30; v++)
                            {
                                using (KetabGardEntities db = new KetabGardEntities())
                                {
                                    user = db.Users.Where(w => w.SaveDateM == i && w.SaveDateD == v).ToList();
                                }
                                result[d] = user.Count;
                                d++;
                            }
                        }

                        if (i == month)
                        {
                            for (int v = 1; v <= day; v++)
                            {
                                using (KetabGardEntities db = new KetabGardEntities())
                                {
                                    user = db.Users.Where(w => w.SaveDateD == v).ToList();
                                }
                                result[d] = user.Count;
                                d++;
                            }
                        }
                    }
                }
            }
            return result;
        }
        private int[] Getdatathisweak()
        {
            int[] result = new int[7];
            List<User> user = new List<User>();
            using (KetabGardEntities db = new KetabGardEntities())
            {
                user = db.Users.ToList();
            }
            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            int[] all = DateConverter.gregorian_to_jalali(year, month, day);

            year = all[0];
            month = all[1];
            day = all[2];

            int[] sevendayago = Getsevendaysago(year, month, day);

            if (year == sevendayago[0])
            {
                if (month == sevendayago[1])
                {
                    int d = 0;
                    for (int i = sevendayago[2]; i <= day; i++)
                    {
                        using (KetabGardEntities db = new KetabGardEntities())
                        {
                            user = db.Users.Where(w => w.SaveDateD == i).ToList();
                        }
                        result[d] = user.Count;
                        d ++;
                    }
                }
                else
                {
                    for (int i = sevendayago[1]; i <= month; i++)
                    {
                        int d = 0;
                        if (i < month)
                        {
                            for (int v = sevendayago[2]; v <= 30; v++)
                            {
                                using (KetabGardEntities db = new KetabGardEntities())
                                {
                                    user = db.Users.Where(w => w.SaveDateM == i && w.SaveDateD == v).ToList();
                                }
                                result[d] = user.Count;
                                d ++;
                            }
                        }
                        else
                        {
                            for (int v = sevendayago[2]; v <= day; v++)
                            {
                                using (KetabGardEntities db = new KetabGardEntities())
                                {
                                    user = db.Users.Where(w => w.SaveDateD == v).ToList();
                                }
                                result[d] = user.Count;
                                d++;
                            }
                        }
                    }
                }
            }
            return result;
        }
        private int Getamanatnotbackcount()
        {
            List<Amanat> amanat = new List<Amanat>();
            using (KetabGardEntities db = new KetabGardEntities())
            {
                amanat = db.Amanats.Where(w => w.Backed == null && w.Active == true).ToList();
            }
            int result = amanat.Count;
            return result;
        }
        private int Getamanatcount()
        {
            List<Amanat> amanat = new List<Amanat>();
            using (KetabGardEntities db = new KetabGardEntities())
            {
                amanat = db.Amanats.ToList();
            }
            int result = amanat.Count;
            return result;
        }
        private int Getuserscount()
        {
            List<User> user = new List<User>();
            using (KetabGardEntities db = new KetabGardEntities())
            {
                user = db.Users.ToList();
            }
            int result = user.Count;
            return result;
        }
        private double Getweakavrage()
        {
            List<User> user = new List<User>();
            using (KetabGardEntities db = new KetabGardEntities())
            {
                user = db.Users.ToList();
            }
            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            int[] all = DateConverter.gregorian_to_jalali(year, month, day);

            year = all[0];
            month = all[1];
            day = all[2];

            if (month - 1 != 0)
            {
                user = user.Where(u => u.SaveDateY == year && u.SaveDateM == month && u.SaveDateD <= day || u.SaveDateM == month -1 && u.SaveDateD >= day).ToList();
            }
            else
            {
                user = user.Where(u => u.SaveDateY == year && u.SaveDateM == month && u.SaveDateD <= day || u.SaveDateY == year-1 && u.SaveDateM == 12 && u.SaveDateD >= day).ToList();
            }

            double result = user.Count;
            return result / 4 ;
        }
        private int[] Getsevendaysago(int year , int month , int day )
        {
            if (Convert.ToDouble(day) - 7 < 0)
            {
                int storeday = 6 - day;
                if (month - 1 != 0)
                {
                    month--;
                    day = 30 - storeday;
                }
                else
                {
                    year--;
                    month = 12;
                    day = 30 - storeday;
                }
            }
            else
            {
                day = day - 6 ;
            }
            int[] result = new int[3];
            result[0] = year;
            result[1] = month;
            result[2] = day;
            return result;
        }
        private void updateusers(List<User> users)
        {
            foreach (var user in users)
            {
                calculatedatetoexpire(user);
            }
        }
        private void calculatedatetoexpire(User user)
        {
            var activetime = user.ActiveTime.Split('/');
            int Ayear = Convert.ToInt32(activetime[0]);
            int Amonth = Convert.ToInt32(activetime[1]);
            int Aday = Convert.ToInt32(activetime[2]);

            var expiretime = user.ExpireTime.Split('/');
            int eyear = Convert.ToInt32(expiretime[0]);
            int emonth = Convert.ToInt32(expiretime[1]);
            int eday = Convert.ToInt32(expiretime[2]);

            if (eyear - Ayear == 0)
            {
                if (emonth - Amonth == 0)
                {
                    if (eday - Aday == 0)
                    {
                        using (KetabGardEntities db = new KetabGardEntities())
                        {
                            user = db.Users.Where(w => w.Id == user.Id).SingleOrDefault();
                            if (user.Expireactivate != 0)
                            {
                                user.Expireactivate = 0;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        int exp = eday - Aday;
                        using (KetabGardEntities db = new KetabGardEntities())
                        {
                            user = db.Users.Where(w => w.Id == user.Id).SingleOrDefault();
                            if (user.Expireactivate != exp)
                            {
                                user.Expireactivate = exp;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    int exp = (emonth - Amonth) * 30;
                    if (eday - Aday == 0)
                    {
                        using (KetabGardEntities db = new KetabGardEntities())
                        {
                            user = db.Users.Where(w => w.Id == user.Id).SingleOrDefault();
                            if (user.Expireactivate != exp)
                            {
                                user.Expireactivate = exp;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                           
                        }
                    }
                    else
                    {
                        exp = (eday - Aday) + exp;
                        using (KetabGardEntities db = new KetabGardEntities())
                        {
                            user = db.Users.Where(w => w.Id == user.Id).SingleOrDefault();
                            if (user.Expireactivate != exp)
                            {
                                user.Expireactivate = exp;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            else
            {
                int exp = (eyear - Ayear) * 365;
                if (emonth - Amonth == 0)
                {
                    if (eday - Aday == 0)
                    {
                        using (KetabGardEntities db = new KetabGardEntities())
                        {
                            user = db.Users.Where(w => w.Id == user.Id).SingleOrDefault();
                            if (user.Expireactivate != exp)
                            {
                                user.Expireactivate = exp;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        exp = (eday - Aday) + exp;
                        using (KetabGardEntities db = new KetabGardEntities())
                        {
                            user = db.Users.Where(w => w.Id == user.Id).SingleOrDefault();
                            if (user.Expireactivate != exp)
                            {
                                user.Expireactivate = exp;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    exp = ((emonth - Amonth) * 30) + exp;
                    if (eday - Aday == 0)
                    {
                        using (KetabGardEntities db = new KetabGardEntities())
                        {
                            user = db.Users.Where(w => w.Id == user.Id).SingleOrDefault();
                            if (user.Expireactivate != exp)
                            {
                                user.Expireactivate = exp;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        exp = (eday - Aday) + exp;
                        using (KetabGardEntities db = new KetabGardEntities())
                        {
                            user = db.Users.Where(w => w.Id == user.Id).SingleOrDefault();
                            if (user.Expireactivate != exp)
                            {
                                user.Expireactivate = exp;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                }
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

        public ActionResult booksearch(string bookname)
        {
            List<Book> books = new List<Book>();
            using (KetabGardEntities db = new KetabGardEntities())
            {
                books = db.Books.Where(w=>w.Name.Contains(bookname) || w.Writer.Contains(bookname)).Take(5).ToList();
            }
            ViewBag.serachof = "کتاب";
            ViewBag.books = books;
            return PartialView("Searchresult");
        }
        public ActionResult usersearch(string nameorcdm)
        {
            List<User> users = new List<User>();
            decimal value;
            Decimal.TryParse(nameorcdm, out value);

            using (KetabGardEntities db = new KetabGardEntities())
            {
                users = db.Users.Where(w => w.LastName.Contains(nameorcdm) || System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)w.MdliCode).Contains(nameorcdm) || w.FirstName.Contains(nameorcdm)).Take(5).ToList();
            }
            ViewBag.users = users;
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
            if (countofmans != 0)
            {
                data.Add((countofmans * 100) / totals);
            }
            else
            {
                data.Add(0);
            }

            if (countofwomans != 0)
            {
                data.Add((countofwomans * 100) / totals);
            }
            else
            {
                data.Add(0);
            }

            return data;
        }

        public JsonResult calculateexpiredate(int id, int day , int month , int year)
        {
            var user = new User();
            using (KetabGardEntities db =new KetabGardEntities())
            {
                user = db.Users.Where(w => w.Id == id).SingleOrDefault();
            }
            if (user.Active == false)
            {
                string activetime = year + "/" + month + "/" + day;
                int expireactivate = 0;
                month += 1;
                if (month > 12)
                {
                    month -= 12;
                    year++;
                }
                string expiredate = year + "/" + month + "/" + day;


                using (KetabGardEntities db = new KetabGardEntities())
                {
                    user = db.Users.Where(w => w.Id == id).SingleOrDefault();
                    expireactivate = Convert.ToInt32(user.Expireactivate);
                    expireactivate += 30;
                    user.ExpireTime = expiredate;
                    user.ActiveTime = activetime;
                    user.Expireactivate = expireactivate;
                    if (user.Active == false)
                    {
                        user.Active = true;
                    }

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                string result = "فعال (" + expireactivate + "روز)";
                return Json(new { success = true, result = result });
            }
            else
            {
                string activetime = user.ActiveTime;
                string expiretime = user.ExpireTime;
                var date = expiretime.Split('/');
                year = Convert.ToInt32(date[0]);
                month = Convert.ToInt32(date[1]);
                day = Convert.ToInt32(date[2]);
                int expireactivate = Convert.ToInt32(user.Expireactivate);
                month += 1;
                if (month > 12)
                {
                    month -= 12;
                    year++;
                }
                string expiredate = year + "/" + month + "/" + day;


                using (KetabGardEntities db = new KetabGardEntities())
                {
                    user = db.Users.Where(w => w.Id == id).SingleOrDefault();
                    expireactivate += 30;
                    user.ExpireTime = expiredate;
                    user.ActiveTime = activetime;
                    user.Expireactivate = expireactivate;
                    if (user.Active == false)
                    {
                        user.Active = true;
                    }

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                string result = "فعال (" + expireactivate + "روز)";
                return Json(new { success = true, result = result });
            }
            
        }

        public JsonResult exit(int id)
        {
            Session.Clear();
            Session.Abandon();
            return Json(new { success = false });
        }
        #endregion

    }
}