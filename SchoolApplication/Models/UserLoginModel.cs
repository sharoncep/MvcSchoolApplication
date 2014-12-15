using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApplication.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Username Required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        //[StringLength(20, MinimumLength = 3)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string Role { get; set; }

    }

    public class UserRegistrationModel
    {
        #region Properties
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Roll No")]
        public string RollNo { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Deparment")]
        public string DepartmentId { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        #endregion

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
        #endregion
    }

    public class UserForgotPasswordModel
    {
        #region Properties
        //[Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Roll No")]
        public string RollNo { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Deparment")]
        public string DepartmentId { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        #endregion

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
        #endregion
    }

    public class UserChangePasswordModel
    {
        #region Properties
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "New password and confirmation does not match.")]
        public string ConfirmPassword { get; set; }


        #endregion
    }

}