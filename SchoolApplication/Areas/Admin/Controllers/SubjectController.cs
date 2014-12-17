using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApplication.Areas.Admin.Controllers
{
    public class SubjectController : Controller
    {
        //
        // GET: /Admin/Subject/

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
