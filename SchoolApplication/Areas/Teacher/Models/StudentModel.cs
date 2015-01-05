using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApplication.Areas.Teacher.Models
{
    public class StudentModel
    {
        #region Properties

        [Required]
        [Display(Name = "Roll No")]
        public string RollNo { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Deparment")]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Date of Enroll")]
        [DataType(DataType.Date)]
        public DateTime DateOfEnroll { get; set; }

        [Required]
        [Display(Name = "Date of Passout")]
        [DataType(DataType.Date)]
        public DateTime DateOfPassout { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        #endregion
    }
}