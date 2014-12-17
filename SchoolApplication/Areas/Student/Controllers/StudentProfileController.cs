using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using SchoolApplication.Areas.Student.Models;

namespace SchoolApplication.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
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

        //[HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangePhoto(StudentProfileModel objModel)
        {
            int maxContent = 1024 * 1024; //1 MB

            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (objModel.ImageUpload == null || objModel.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
            }
            else if (!validImageTypes.Contains(objModel.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }
            else if (objModel.ImageUpload.ContentLength > maxContent)
            {
                ModelState.AddModelError("ImageUpload", "Please upload an image having size less than 1 MB");
            }

            if (ModelState.IsValid)
            {
                if (objModel.ImageUpload != null && objModel.ImageUpload.ContentLength > 0)
                {
                    //string uploadDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ProfilePhotos\Students\");
                    string uploadDir = HostingEnvironment.MapPath("~/ProfilePhotos/Students/");
                    var extension = Path.GetExtension(objModel.ImageUpload.FileName);
                    var fileName = User.Identity.Name + "_photo";
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);
                    var imageUrl = uploadDir + fileName + extension;
                    objModel.ImageUpload.SaveAs(imageUrl);
                    using (var ctx = new SchoolDBContext())
                    {
                        imageUrl = "~/ProfilePhotos/Students/" + fileName + extension;
                        objModel.StudentPersonalDetails = (from stud in ctx.Students join log in ctx.Logins on stud.StudentId equals log.UserId where log.UserName == User.Identity.Name select stud).FirstOrDefault();
                        if (objModel.StudentPersonalDetails != null)
                        {
                            objModel.StudentPersonalDetails.PhotoPath = imageUrl;
                            ctx.SaveChanges();
                        }
                    }
                    return RedirectToAction("PersonalProfile", "StudentProfile");
                }
            }
            return View(objModel);
        }

        [HttpGet]
        public ActionResult PersonalProfile()
        {
            var objModel = new StudentProfileModel();
            objModel.GetPersonalDetails(User.Identity.Name);
            //return View(objModel);
            return PartialView(objModel);
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
        public ActionResult ChangePassword(UserChangePasswordModel objModel)
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
