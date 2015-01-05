using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolApplication.Models;

namespace SchoolApplication.Areas.Admin.Models
{
    public class TeacherModel
    {
        #region Properties

        public string TeacherId { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOJ { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        public string PhotoPath { get; set; }

        #endregion

        #region Public Functions

        public IEnumerable<SelectListItem> GetDepartments()
        {
            var objCommon = new CommonModel();
            return objCommon.GetDepartments();
        }

        public IEnumerable<SelectListItem> GetSubjects()
        {
            var objCommon = new CommonModel();
            return objCommon.GetSubjects();
        }

        public IEnumerable<SelectListItem> GetGender()
        {
            var objCommon = new CommonModel();
            return objCommon.GetGender();
        }

        #endregion
    }
}