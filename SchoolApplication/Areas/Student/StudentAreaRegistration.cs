using System.Web.Mvc;

namespace SchoolApplication.Areas.Student
{
    public class StudentAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Student";
            }
        }

        /// <summary>
        /// http://www.codeproject.com/Articles/714356/Areas-in-ASP-NET-MVC
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Student_default",
                "Student/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                , new[] { "SchoolApplication.Areas.Student.Controllers" }
            );
        }
    }
}
