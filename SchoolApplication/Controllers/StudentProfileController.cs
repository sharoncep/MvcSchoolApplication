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
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(Models.UserChangePasswordModel objModel)
        {
            if (ModelState.IsValid)
            {

            }

            return View(objModel);
        }

    }
}
