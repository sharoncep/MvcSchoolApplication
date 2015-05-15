using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolApplication.Areas.Admin;

namespace SchoolApplication.Areas.Admin.Controllers
{
    public class SubjectController : Controller
    {
        //
        // GET: /Admin/Subject/

        public ActionResult Listing()
        {
            var objModel = new Models.SubjectModel();
            objModel.GetSubjects();
            return View(objModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var objModel = new Models.SubjectModel();
            objModel.IsActive = true;
            return View(objModel);
        }

        [HttpPost]
        public ActionResult Add(Models.SubjectModel objModel)
        {
            if (ModelState.IsValid)
            {
                //foreach (var subj in objModel)
                //{
                //    Response.Write(subj.SubjectName + " " + subj.IsActive + "<br>");
                //}
                using (var ctx = new SchoolDBContext())
                {
                    var subjectDb =
                        ctx.Subjects.FirstOrDefault(
                            x => x.SubjectName == objModel.SubjectName && x.DepartmentId == objModel.DepartmentId);
                    if (subjectDb == null)
                    {
                        var subject = ctx.Subjects.Create();
                        subject.SubjectCode = objModel.SubjectCode;
                        subject.SubjectName = objModel.SubjectName;
                        subject.DepartmentId = objModel.DepartmentId;
                        subject.IsActive = objModel.IsActive;
                        ctx.Subjects.Add(subject);
                        ctx.SaveChanges();
                        ViewBag.message = "Subject " + objModel.SubjectName + " added successfully";
                    }
                    else
                        ModelState.AddModelError("", "Subject already exist for this department.");
                }
            }
            return View(objModel);
        }

        //public ViewResult BlankEditorRow()
        //{
        //    return View("Subject", new Models.Subj());
        //}

        public ActionResult Edit(int Id)
        {
            var objModel = new Models.SubjectModel();

            using (var ctx = new SchoolDBContext())
            {
                var subject = ctx.Subjects.FirstOrDefault(x => x.SubjectId == Id);
                if (subject != null)
                {
                    objModel.SubjectId = subject.SubjectId;
                    objModel.DepartmentId = subject.DepartmentId;
                    objModel.SubjectCode = subject.SubjectCode;
                    objModel.SubjectName = subject.SubjectName;
                    objModel.IsActive = subject.IsActive;
                    objModel.HiddenValue = subject.SubjectName;
                }
            }

            return View(objModel);
        }

        [HttpPost]
        public ActionResult Edit(Models.SubjectModel objModel)
        {

            if (ModelState.IsValid)
            {
                using (var ctx = new SchoolDBContext())
                {
                    Subject subjectName = null;

                    if (objModel.SubjectName != objModel.HiddenValue)
                    {
                        subjectName =
                           ctx.Subjects.FirstOrDefault(x => x.SubjectName == objModel.SubjectName && x.DepartmentId == objModel.DepartmentId);
                    }

                    if (subjectName == null)
                    {
                        var subject = ctx.Subjects.FirstOrDefault(x => x.SubjectId == objModel.SubjectId && x.DepartmentId == objModel.DepartmentId);
                        subject.SubjectCode = objModel.SubjectCode;
                        subject.SubjectName = objModel.SubjectName;
                        subject.DepartmentId = objModel.DepartmentId;
                        subject.IsActive = objModel.IsActive;

                        ctx.SaveChanges();

                        ViewBag.message = "Subject updated successfully";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Subject already exist for this department");
                        return View(objModel);

                    }
                }
            }
            return RedirectToAction("Listing");
        }

    }
}
