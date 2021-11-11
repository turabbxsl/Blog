using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogsayt.Controllers
{
    public class YetkiliController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["username"] == null)
            {
                //filterContext.Result = new RedirectResult("Home");

                filterContext.Result = new HttpNotFoundResult();
                return;
            }
            base.OnActionExecuting(filterContext);
        }
        public ActionResult Xeta(string yazilacaq)
        {
            ViewBag.yaz = yazilacaq;
            return View();
        }
    }
}