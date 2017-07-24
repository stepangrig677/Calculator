using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Calculator.Models;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Калькулятор";
            ResultModels model = new ResultModels();
            return View(model);
        }

        [HttpPost]

        public ActionResult Index(ResultModels model)
        {
            ViewBag.Title = "Калькулятор";

            return View(model);
        }
    }
}
