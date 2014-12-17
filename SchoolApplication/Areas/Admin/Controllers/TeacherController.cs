using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApplication.Areas.Admin.Controllers
{
    public class TeacherController : Controller
    {
        //
        // GET: /Admin/TeacherMgmt/

        public ActionResult Listing()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

    }
}
