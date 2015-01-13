using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SchoolApplication.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            if (Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("LogIn", "User", new { area = "" });
            }
            return View();
        }

        /// <summary>
        /// http://stackoverflow.com/questions/1822548/mvc-how-to-store-assign-roles-of-authenticated-users
        /// </summary>
        /// <param name="objUserLogin"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogIn(Models.UserLoginModel objUserLogin)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(objUserLogin.UserName, objUserLogin.Password))
                {
                    var objRole = new UtilityClass.MyRoleProvider();
                    FormsAuthentication.SetAuthCookie(objUserLogin.UserName, false);
                    var roles = objRole.GetRolesForUser(objUserLogin.UserName);

                    if (roles.Contains("Student"))
                        return RedirectToAction("Home", "StudentProfile", new { area = "Student" });
                    if (roles.Contains("Teacher"))
                        return RedirectToAction("Home", "TeacherProfile", new { area = "Teacher" });
                    if (roles.Contains("Admin"))
                        return RedirectToAction("Home", "AdminProfile", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                }
            }
            return View(objUserLogin);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("LogIn", "User", new { area = "" });
        }

        [HttpGet]
        public ActionResult Registration()
        {
            var objModel = new Models.UserRegistrationModel();
            //objModel.GetDepartments();
            return View(objModel);
        }

        [HttpPost]
        public ActionResult Registration(Models.UserRegistrationModel objRegistration)
        {
            if (ModelState.IsValid)
            {
                var crypt = new SimpleCrypto.PBKDF2();
                var password = UtilityClass.RandomPassword.Generate(5);
                var cryptPassword = crypt.Compute(password);
                using (var ctx = new SchoolDBContext())
                {
                    var username = ctx.Logins.FirstOrDefault(x => x.UserName == objRegistration.UserName);

                    if (username == null)
                    {
                        var student =
                            ctx.Students.FirstOrDefault(
                                x =>
                                    x.RollNo == objRegistration.RollNo && x.DOB == objRegistration.Dob &&
                                    x.Email == objRegistration.Email);
                        if (student != null)
                        {
                            username = ctx.Logins.FirstOrDefault(x => x.UserId == student.StudentId);
                            if (username == null)
                            {
                                try
                                {
                                    var user = ctx.Logins.Create();
                                    user.UserName = objRegistration.UserName;
                                    user.Password = cryptPassword;
                                    user.PasswordSalt = crypt.Salt;
                                    user.Role = UtilityClass.RolesList.STUDENT.Value;
                                    user.IsActive = true;
                                    user.UserId = student.StudentId;

                                    ctx.Logins.Add(user);
                                    ctx.SaveChanges();
                                    UtilityClass.Messenger.SendMail(objRegistration.Email, "Your School Account Details",
                                        EmailBody(objRegistration.UserName, password).ToString(), true);
                                    ViewBag.SuccessMessage =
                                        "Registartion Successful. Please check your mail for login details.";
                                }
                                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                                {
                                    Exception raise = dbEx;
                                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                                    {
                                        foreach (var validationError in validationErrors.ValidationErrors)
                                        {
                                            string message = string.Format("{0}:{1}",
                                                validationErrors.Entry.Entity.ToString(),
                                                validationError.ErrorMessage);
                                            // raise a new exception nesting  
                                            // the current instance as InnerException  
                                            raise = new InvalidOperationException(message, raise);
                                        }
                                    }
                                    throw raise;
                                }
                            }
                            else
                                ModelState.AddModelError("", "This student already assigned a login. If you cant login please use forgot Password to get your username and password");
                        }
                        else
                        {
                            ModelState.AddModelError("",
                                "User details not found in database. Make sure entered details match with your details in school records");
                        }
                    }
                    else
                        ModelState.AddModelError("", "Username already exist. Please try with another username");
                }

            }
            return View(objRegistration);
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            var objModel = new Models.UserForgotPasswordModel();
            return View(objModel);
        }

        [HttpPost]
        public ActionResult ForgotPassword(Models.UserForgotPasswordModel objModel)
        {
            if (ModelState.IsValid)
            {
                var crypt = new SimpleCrypto.PBKDF2();
                var password = UtilityClass.RandomPassword.Generate(5);
                var cryptPassword = crypt.Compute(password);
                using (var ctx = new SchoolDBContext())
                {

                    var student =
                        ctx.Students.FirstOrDefault(
                            x =>
                                x.RollNo == objModel.RollNo && x.DOB == objModel.Dob &&
                                x.Email == objModel.Email);
                    if (student != null)
                    {
                        var username = ctx.Logins.FirstOrDefault(x => x.UserId == student.StudentId);
                        if (username != null)
                        {
                            try
                            {
                                username.Password = cryptPassword;
                                username.PasswordSalt = crypt.Salt;
                                ctx.SaveChanges();

                                UtilityClass.Messenger.SendMail(objModel.Email, "Password Reset",
                                    EmailBody(username.UserName, password).ToString(), true);
                                ViewBag.SuccessMessage =
                                    "Password reset. Please check your mail for login details.";
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                            {
                                Exception raise = dbEx;
                                foreach (var validationErrors in dbEx.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        string message = string.Format("{0}:{1}",
                                            validationErrors.Entry.Entity.ToString(),
                                            validationError.ErrorMessage);
                                        // raise a new exception nesting  
                                        // the current instance as InnerException  
                                        raise = new InvalidOperationException(message, raise);
                                    }
                                }
                                throw raise;
                            }
                        }
                        else
                            ModelState.AddModelError("", "This student dosen't have a valid login. Please register to login");
                    }
                    else
                    {
                        ModelState.AddModelError("",
                            "User details not found in database. Make sure entered details match with your details in school records");
                    }

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
                        //Session["Role"] = user.Role;
                    }
                }
            }

            return isValid;
        }

        private StringBuilder EmailBody(string username, string password)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<h3>Please login with these credentials</h3>");
            sb.AppendLine("<p>Username : " + username + " </p>");
            sb.AppendLine("<p>Password : " + password + "</p>");
            sb.AppendLine("<p>Login Url : </p>");
            sb.AppendLine("<p>Thank you</p>");

            return sb;

        }

        [HttpGet]
        public ActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAdmin(Models.UserLoginModel objModel)
        {
            if (ModelState.IsValid)
            {
                var crypt = new SimpleCrypto.PBKDF2();
                string password = crypt.Compute(objModel.Password);
                using (var ctx = new SchoolDBContext())
                {
                    var userName = ctx.Logins.FirstOrDefault(x => x.UserName == objModel.UserName);
                    if (userName == null)
                    {
                        var user = ctx.Logins.Create();
                        user.UserName = objModel.UserName;
                        user.Password = password;
                        user.PasswordSalt = crypt.Salt;
                        user.Role = UtilityClass.RolesList.ADMIN.Value;
                        user.UserId = null;
                        user.IsActive = true;

                        ctx.Logins.Add(user);
                        ctx.SaveChanges();
                        ViewBag.message = "Administator login created successfully";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username already exists");
                    }
                }
            }

            return View(objModel);
        }
    }
}
