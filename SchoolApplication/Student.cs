//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolApplication
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public int StudentId { get; set; }
        public string RollNo { get; set; }
        public string Name { get; set; }
        public System.DateTime DOB { get; set; }
        public int DepartmentId { get; set; }
        public string Gender { get; set; }
        public System.DateTime DateOfEnroll { get; set; }
        public Nullable<System.DateTime> DateOfPassOut { get; set; }
        public string PhotoPath { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
    
        public virtual Department Department { get; set; }
    }
}
