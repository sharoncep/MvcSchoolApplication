using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApplication.Controllers
{
    public class StudentProfileController : Controller
    {
        //
        // GET: /StudentProfile/

        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangePhoto()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PersonalProfile()
        {
            Models.StudentProfileModel objModel = new Models.StudentProfileModel();
            objModel.GetPersonalDetails(User.Identity.Name);
            return View(objModel);
        }

        [HttpGet]
        public ActionResult AcademicProfile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(Models.UserChangePasswordModel objModel)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.User.Identity.Name;
                var password = objModel.CurrentPassword;
                if (IsValid(userName, password))
                {
                    using (var ctx = new SchoolDBContext())
                    {
                        var user = ctx.Logins.FirstOrDefault(x => x.UserName == userName);
                        if (user != null)
                        {
                            var crypt = new SimpleCrypto.PBKDF2();
                            user.Password = crypt.Compute(objModel.NewPassword);
                            user.PasswordSalt = crypt.Salt;
                            ctx.SaveChanges();

                            ViewBag.SuccessMessage = "Password changed successfully.";
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Current password is incorrect");
                }
            }

            return View(objModel);
        }

        private bool IsValid(string userName, string password)
        {
            bool isValid = false;
            var crypto = new SimpleCrypto.PBKDF2();

            using (var db = new SchoolDBContext())
            {
                var user = db.Logins.FirstOrDefault(u => u.UserName == userName);
                if (user != null)
                {
                    if (user.Password == crypto.Compute(password, user.PasswordSalt))
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }
    }
}
