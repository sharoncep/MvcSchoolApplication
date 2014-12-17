using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolApplication.Areas.Admin;

namespace SchoolApplication.Areas.Admin.Controllers
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Admin/Department/

        public ActionResult Listing()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Models.DepartmentModel objModel)
        {
            if (ModelState.IsValid)
            {
                objModel.SaveDepartment(objModel);
            }
            return View(objModel);
        }

        public ActionResult Edit()
        {
            return View();
        }

    }
}
