using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApplication.Models
{
    public class CommonModel
    {
        #region Public Functions

        public IEnumerable<SelectListItem> GetDepartments()
        {

            using (var ctx = new SchoolDBContext())
            {
                //DepartmentList = 
                return ctx.Departments.ToList().Select(x =>
                    new SelectListItem
                    {
                        Value = x.DepartmentId.ToString(),
                        Text = x.DepartmentName
                    });
            }
        }

        public IEnumerable<SelectListItem> GetSubjects()
        {

            using (var ctx = new SchoolDBContext())
            {
                //DepartmentList = 
                return ctx.Subjects.ToList().Select(x =>
                    new SelectListItem
                    {
                        Value = x.SubjectId.ToString(),
                        Text = x.SubjectName
                    });
            }
        }


        public IEnumerable<SelectListItem> GetGender()
        {

            var li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Male", Value = "Male" });
            li.Add(new SelectListItem { Text = "Female", Value = "Female" });
            return li;
        }

        #endregion
    }
}