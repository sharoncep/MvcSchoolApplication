using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolApplication.Areas.Admin.Models
{
    public class TeacherModel
    {
        #region Properties

        [Required]
        public string Name { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string Email { get; set; }

        #endregion
    }
}