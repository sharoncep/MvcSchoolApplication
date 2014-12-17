using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolApplication.Areas.Admin.Models
{
    public class DepartmentModel
    {
        #region Properties

        [Required]
        public string DepartmentName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        #endregion

        #region Public Functions

        public void SaveDepartment(DepartmentModel objDept)
        {
            using (var ctx = new SchoolDBContext())
            {
                var department = ctx.Departments.Create();
                department.DepartmentName = objDept.DepartmentName;
                department.IsActive = objDept.IsActive;

                ctx.Departments.Add(department);
                ctx.SaveChanges();
            }
        }

        #endregion
    }
}