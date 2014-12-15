using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolApplication.Models
{
    public class StudentProfileModel
    {
        #region Properties

        public string Department { get; set; }

        public Student StudentPersonalDetails { get; set; }
        #endregion

        #region Public Functions

        public void GetPersonalDetails(string userName)
        {
            using (var ctx = new SchoolDBContext())
            {
                StudentPersonalDetails = (from stud in ctx.Students join log in ctx.Logins on stud.StudentId equals log.UserId where log.UserName == userName select stud).FirstOrDefault();
                if (StudentPersonalDetails != null)
                    Department =
                        ctx.Departments.FirstOrDefault(x => x.DepartmentId == StudentPersonalDetails.DepartmentId)
                            .DepartmentName;
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