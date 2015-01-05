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
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            var objModel = new Models.SubjectModel();
            return View(objModel);
        }

        [HttpPost]
        public ActionResult Add(Models.SubjectModel objModel)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new SchoolDBContext())
                {
                    var subjectDb =
                        ctx.Subjects.FirstOrDefault(
                            x => x.SubjectName == objModel.SubjectName && x.DepartmentId == objModel.DepartmentId);
                    if (subjectDb == null)
                    {
                        var subject = ctx.Subjects.Create();
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

        public ActionResult Edit()
        {
            return View();
        }

    }
}
