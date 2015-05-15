using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApplication.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return View("Error");
                }

                if (User.IsInRole("Student"))
                {
                }
                if (User.IsInRole("Teacher"))
                {
                }
            }

            return View("Error");
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = 404;  //you may want to set this to 200
            return View("NotFound");
        }

        public ViewResult UnauthorizedAccess()
        {
            Response.StatusCode = 403;  //you may want to set this to 200
            return View("UnauthorizedAccess");
        }

    }
}
