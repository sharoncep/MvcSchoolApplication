using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApplication.Areas.Teacher.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Teacher/Student/

        public ActionResult Listing()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            var objModel = new Models.StudentModel();
            return View(objModel);
        }

        [HttpPost]
        public ActionResult Add(Models.StudentModel objModel)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new SchoolDBContext())
                {
                    var studentName = ctx.Students.FirstOrDefault(x => x.RollNo == objModel.RollNo);
                    if (studentName == null)
                    {
                        var newStudent = ctx.Students.Create();
                        newStudent.RollNo = objModel.RollNo;
                        newStudent.Name = objModel.Name;
                        newStudent.DOB = objModel.Dob;
                        newStudent.DepartmentId = objModel.DepartmentId;
                        newStudent.Gender = objModel.Gender;
                        newStudent.Email = objModel.Email;
                        newStudent.DateOfEnroll = objModel.DateOfEnroll;
                        newStudent.DateOfPassOut = objModel.DateOfPassout;
                        newStudent.IsActive = objModel.IsActive;

                        ctx.Students.Add(newStudent);
                        ctx.SaveChanges();
                    }
                    else
                        ModelState.AddModelError("", "Roll No already exist.");


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
