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
    
    public partial class Login
    {
        public int LoginId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Nullable<int> UserId { get; set; }
        public bool IsActive { get; set; }
        public string PasswordSalt { get; set; }
    }
}
