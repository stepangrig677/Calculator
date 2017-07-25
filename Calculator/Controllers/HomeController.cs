using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Calculator.Models;
using Calculator.CalcultorDb;


namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        private CalclatorDbContext DB = new CalclatorDbContext();
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
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (System.Web.HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            }


            ViewBag.Title = "Калькулятор";
            Log log = new Log
            {
                ID = 1,
                OperationTime = System.DateTime.Now,
                IpAddress = ip,
                Operands = model.Operations
            };
            DB.Logs.Add(log);
            DB.SaveChanges();
            try
            {
                log.Result = Caclulate(model.Operations);
                model.Operations=log.Result.ToString();
                DB.Logs.Add(log);
                DB.SaveChanges();
                return View(model);
            }
            catch
            {
                log.Result = 0;
                DB.Logs.Add(log);
                DB.SaveChanges();
                return View("Error");
            }

            //log.Result = model.Operations;
            
        }

        #region Helpers

        private static decimal Operation(decimal A, decimal B, char Operand)
        {
            decimal result;
            switch (Operand)
            {
                case '+':
                    result = A + B;
                    break;
                case '-':
                    result = A - B;
                    break;
                case '*':
                    result = A * B;
                    break;
                case '/':
                    result = A / B;
                    break;
                default:
                    result = A + B;
                    break;
            }
            return result;
        }
        private static decimal Caclulate(string input)
        {
            decimal result;
            decimal previos = 0;
            string current = "";
            char operand = '+';
            foreach (char x in input)
            {
                if (x == '+' || x == '-' || x == '*' || x == '/')
                {
                    previos = Operation(previos, Decimal.Parse(current), operand);
                    operand = x;
                    current = "";
                }
                else
                {
                    current += x;
                }
            }
            result = Operation(previos, Decimal.Parse(current), operand);
            return result;
        }
        #endregion
    }
}
