﻿using System;
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
    }
}