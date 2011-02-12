using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigISLoser.ViewModels;
using BigISLoser.Repositories;
using BigISLoser.Models;

namespace BigISLoser.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(new HomeIndexViewModel());
        }
    }
}
