using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepoVKR.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            if (!Util.GetIsUserInAccepted(Request.LogonUserIdentity.Name))
                return new HttpStatusCodeResult(403);

            return View();
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            if (!Util.GetIsUserInAccepted(Request.LogonUserIdentity.Name))
                return new HttpStatusCodeResult(403);

            return View();
        }

        public ActionResult Contact()
        {
            if (!Util.GetIsUserInAccepted(Request.LogonUserIdentity.Name))
                return new HttpStatusCodeResult(403);

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
