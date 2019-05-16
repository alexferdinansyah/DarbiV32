using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    [AllowAnonymous]
    public class UnauthorizedController : Controller
    {
        // GET: Unauthorized
        public ActionResult Index(String url)
        {
            ViewBag.url = url;

            return View();
        }
    }
}