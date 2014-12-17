using System.Web.Mvc;

namespace SchoolApplication.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProfileController : Controller
    {
        //
        // GET: /AdminProfile/

        public ActionResult Home()
        {
            return View();
        }


    }
}
