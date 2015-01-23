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
            var objModel = new Models.DepartmentModel();
            objModel.GetDepartment();
            return View(objModel);
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
                        department.DepartmentCode = objModel.DepartmentCode;
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

        public ActionResult Edit(int Id)
        {
            var objModel = new Models.DepartmentModel();

            using (var ctx = new SchoolDBContext())
            {
                var department = ctx.Departments.FirstOrDefault(x => x.DepartmentId == Id);
                if (department != null)
                {
                    objModel.DepartmentId = department.DepartmentId;
                    objModel.DepartmentCode = department.DepartmentCode;
                    objModel.DepartmentName = department.DepartmentName;
                    objModel.IsActive = department.IsActive;
                    objModel.HiddenValue = department.DepartmentName;
                }
            }

            return View(objModel);
        }

        [HttpPost]
        public ActionResult Edit(Models.DepartmentModel objModel)
        {

            if (ModelState.IsValid)
            {
                using (var ctx = new SchoolDBContext())
                {
                    Department departmentName = null;

                    if (objModel.DepartmentName != objModel.HiddenValue)
                    {
                        departmentName =
                           ctx.Departments.FirstOrDefault(x => x.DepartmentName == objModel.DepartmentName);
                    }

                    if (departmentName == null)
                    {
                        var department = ctx.Departments.FirstOrDefault(x => x.DepartmentId == objModel.DepartmentId);
                        department.DepartmentCode = objModel.DepartmentCode;
                        department.DepartmentName = objModel.DepartmentName;
                        department.IsActive = objModel.IsActive;

                        ctx.SaveChanges();

                        ViewBag.message = "Department updated successfully";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Department name already exist");
                        return View(objModel);

                    }
                }
            }
            return RedirectToAction("Listing");
        }

    }
}
