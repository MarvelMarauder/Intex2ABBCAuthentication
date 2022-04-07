using Intex2ABBCAuthentication.Models;
using Intex2ABBCAuthentication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Intex2ABBCAuthentication.Controllers
{
    public class HomeController : Controller
    {
        public string City { get; set; }
        public string County { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double Severity { get; set; }
        public CrashFilter filter { get; set; }

        private readonly ILogger<HomeController> _logger;
        private ICrashRepository repo;

        public HomeController(ILogger<HomeController> logger, ICrashRepository temp)
        {
            _logger = logger;
            repo = temp;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SummaryInitial()
        {
            if (filter is null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SummaryData", new { c = filter });
            }
        }
        

        //[HttpGet]
        //public IActionResult SummaryData(int pageNum = 1)
        //{
        //    ViewBag.PageNum = pageNum;
        //    return View();
        //}
        [HttpPost]
        public IActionResult SummaryData(CrashFilter c)
        {
            filter = c;
            int month = c.month;
            int year = c.year;
            string city = c.city;
            string county = c.county;
            double severity = c.severity;

            var queryCrash = from crash in repo.Crashes
                                       where 
                                       crash.crash_date.Month == month && 
                                       crash.crash_date.Year == year &&
                                       crash.city == city &&
                                       crash.county_name == county &&
                                       crash.crash_severity_id == severity
                                       select crash;

            var things = new IntegersUsed(repo, 10)
            {
                Crashes2 = queryCrash
            };

            return View(things);

        }
        

        [HttpGet]
        public IActionResult Details(int fieldid)
        {
            var blah = repo.Crashes.Single(x => x.Field1 == fieldid);
            return View(blah);
        }

        [HttpGet]
        public IActionResult AddCrash()
        {
            return View("EditAdd");
        }
        [HttpPost]
        public IActionResult AddCrash(CarCrash c)
        {
            if (ModelState.IsValid)
            {
                var i = repo.Crashes.Count();
                c.Field1 = i + 1;
                repo.CreateCrash(c);
                return View("Confirmation", c);
            }
            else //if invalid, send back to the form and see error messages
            {
                return View("EditAdd",c);
            }
        }
        [HttpGet]
        public IActionResult EditCrash(int fieldid)
        {
            var stuff = repo.Crashes.Single(x => x.Field1 == fieldid);
            return View("EditAdd", stuff);
        }
        [HttpPost]
        public IActionResult EditCrash(CarCrash c)
        {
            if (ModelState.IsValid)
            {
                repo.SaveCrash(c);
                return View("Confirmation", c);
            }
            else
            {
                return View("AddEdit", c);
            }


            return RedirectToAction("EditAdd", c);
        }

        [HttpGet]
        public IActionResult Delete(int fieldId)
        {
            var to_delete = repo.Crashes.Single(x => x.Field1 == fieldId);

            return View(to_delete);
        }

        [HttpPost]
        public IActionResult Delete(CarCrash cc, int fieldId)
        {
            var carCrash = repo.Crashes.Single(x => x.Field1== fieldId);

            repo.DeleteCrash(carCrash);

            return RedirectToAction("SummaryData");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
