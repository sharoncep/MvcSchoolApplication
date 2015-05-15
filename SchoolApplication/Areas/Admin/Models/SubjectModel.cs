using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApplication.Areas.Admin.Models
{
    public class Subj
    {
        public string SubjectName { get; set; }
        public bool IsActive { get; set; }
    }

    public class SubjectList
    {
        public int SubjectId { get; set; }

        public string SubjectCode { get; set; }

        public string SubjectName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public bool IsActive { get; set; }
    }

    public class SubjectModel
    {
        #region Properties


        public string HiddenValue { get; set; }

        public int SubjectId { get; set; }

        [Required]
        public string SubjectCode { get; set; }

        [Required]
        public string SubjectName { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public IEnumerable<SelectListItem> DepartmentList { get; set; }

        public List<SubjectList> SubjectList { get; set; }

        //public IEnumerable<Subj> Subjects { get; set; }


        #endregion

        #region Public Functions

        public IEnumerable<SelectListItem> GetDepartment()
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

        public List<SubjectList> GetSubjects()
        {
            using (var ctx = new SchoolDBContext())
            {
                //SubjectList = ctx.Subjects.OrderByDescending(x => x.SubjectId).ToList();
                SubjectList = (from sub in ctx.Subjects
                               join dept in ctx.Departments
                                   on sub.DepartmentId equals dept.DepartmentId into departmentList
                               from departments in departmentList.DefaultIfEmpty()
                               select new SubjectList
                               {
                                   SubjectId = sub.SubjectId
                                   ,
                                   SubjectCode = sub.SubjectCode
                                   ,
                                   SubjectName = sub.SubjectName
                                   ,
                                   DepartmentId = sub.DepartmentId
                                   ,
                                   DepartmentName = departments.DepartmentName
                                   ,
                                   IsActive = departments.IsActive
                               }).ToList();
            }

            return SubjectList;
        }

        #endregion
    }
}