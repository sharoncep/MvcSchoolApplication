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

        [HttpGet]
        public ActionResult Add()
        {
            var objModel = new Models.TeacherModel();
            return View(objModel);
        }

        [HttpPost]
        public ActionResult Add(Models.TeacherModel objModel)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new SchoolDBContext())
                {
                    var teacherName = ctx.Teachers.FirstOrDefault(x => x.EmployeeId == objModel.EmployeeId);
                    if (teacherName == null)
                    {
                        var crypt = new SimpleCrypto.PBKDF2();
                        var password = UtilityClass.RandomPassword.Generate(5);
                        var cryptPassword = crypt.Compute(password);

                        var newTeacher = ctx.Teachers.Create();
                        newTeacher.Name = objModel.Name;
                        newTeacher.DepartmentId = objModel.DepartmentId;
                        newTeacher.Gender = objModel.Gender;
                        newTeacher.DOB = objModel.DOB;
                        newTeacher.SubjectId = objModel.SubjectId;
                        newTeacher.Email = objModel.Email;
                        newTeacher.IsActive = objModel.IsActive;
                        ctx.Teachers.Add(newTeacher);
                        ctx.SaveChanges();
                        ViewBag.message = "Teacher, " + objModel.Name + " added successfully";
                    }
                    else
                        ModelState.AddModelError("", "Employee Id already exist.");
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
