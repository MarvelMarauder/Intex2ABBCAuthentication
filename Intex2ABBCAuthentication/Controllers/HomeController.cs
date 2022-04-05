using Intex2ABBCAuthentication.Models;
using Intex2ABBCAuthentication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2ABBCAuthentication.Controllers
{
    public class HomeController : Controller
    {
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

        public IActionResult SummaryData(int pageNum = 1)
        {
            int pageSize = 300;

            var x = new CrashViewModel
            {
                //CarCrashes = repo.Crashes
                //.Where(b => b.Category == category || category == null)
                //.Skip((pageNum - 1) * pageSize)
                //.Take(pageSize),

                //Link Bullocks form here

                //PageInfo = new PageInfo
                //{
                //    TotalNumCrashes =
                //        (category == null
                //        ? repo.Crashes.Count()
                //        : repo.Crashes.Where(x => x.Category == category).Count()),
                //    CrashesPerPage = pageSize,
                //    CurrentPage = pageNum
                //}

            };

            return View(x);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
