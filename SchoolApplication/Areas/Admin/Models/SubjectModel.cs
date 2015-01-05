using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApplication.Areas.Admin.Models
{
    public class SubjectModel
    {
        #region Properties

        [Required]
        public string SubjectName { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public IEnumerable<SelectListItem> DepartmentList { get; set; }

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
        #endregion
    }
}