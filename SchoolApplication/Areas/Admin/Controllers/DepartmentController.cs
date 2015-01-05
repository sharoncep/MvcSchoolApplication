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
                using (var ctx = new SchoolDBContext())
                {
                    var departmentName =
                        ctx.Departments.FirstOrDefault(x => x.DepartmentName == objModel.DepartmentName);
                    if (departmentName == null)
                    {
                        var department = ctx.Departments.Create();
                        department.DepartmentName = objModel.DepartmentName;
                        department.IsActive = objModel.IsActive;

                        ctx.Departments.Add(department);
                        ctx.SaveChanges();
                        ViewBag.message = "Department " + objModel.DepartmentName + " added successfully";
                    }
                    else
                        ModelState.AddModelError("", "Department name already exist");
                }
            }
            return View(objModel);
        }

        public ActionResult Edit()
        {
            return View();
        }

    }
}
