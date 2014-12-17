using System.Web.Mvc;

namespace SchoolApplication.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherProfileController : Controller
    {
        //
        // GET: /TeacherProfile/

        public ActionResult Home()
        {
            return View();
        }

    }
}
