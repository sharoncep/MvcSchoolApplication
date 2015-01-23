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

        public string HiddenValue { get; set; }

        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentCode { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public List<Department> DrptList { get; set; }
        #endregion

        #region Public Functions

        public List<Department> GetDepartment()
        {
            using (var ctx = new SchoolDBContext())
            {
                DrptList = ctx.Departments.OrderByDescending(x => x.DepartmentId).ToList();
            }
            return DrptList;
        }
        #endregion
    }
}