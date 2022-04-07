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
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

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
        private InferenceSession _session;

        public HomeController(ILogger<HomeController> logger, ICrashRepository temp, InferenceSession session)
        {
            _logger = logger;
            repo = temp;
            _session = session;

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
        public IActionResult Prediction(Prediction prediction)
        {
            var stuff = new Prediction { PredictedValue = (float)0.0 };
            ViewBag.Pred = stuff;
            return View();
        }
        [HttpPost]
        public IActionResult Prediction(CarCrash data)
        { 
            var result = _session.Run(new List<NamedOnnxValue>
                {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
                });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First() };
            result.Dispose();

            var stuff = prediction;
            ViewBag.Pred = stuff;

            return View(data);
        }

        //[HttpGet]
        //public IActionResult SummaryInitial()
        //{
        //    var things = new IntegersUsed(repo, 10)
        //    {
        //        Crashes2 = repo.Crashes.AsEnumerable()
        //    };

        //    return View("SummaryData", things);
        //}

        public PageInfo GetPageInfo(int totalItems, int currentPage = 1,int pageSize = 10,int maxPages = 10)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

            // ensure current page isn't out of range
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            int startPage, endPage;
            if (totalPages <= maxPages)
            {
                // total pages less than max so show all pages
                startPage = 1;
                endPage = totalPages;
            }
            else
            {
                // total pages more than max so calculate start and end pages
                var maxPagesBeforeCurrentPage = (int)Math.Floor((decimal)maxPages / (decimal)2);
                var maxPagesAfterCurrentPage = (int)Math.Ceiling((decimal)maxPages / (decimal)2) - 1;
                if (currentPage <= maxPagesBeforeCurrentPage)
                {
                    // current page near the start
                    startPage = 1;
                    endPage = maxPages;
                }
                else if (currentPage + maxPagesAfterCurrentPage >= totalPages)
                {
                    // current page near the end
                    startPage = totalPages - maxPages + 1;
                    endPage = totalPages;
                }
                else
                {
                    // current page somewhere in the middle
                    startPage = currentPage - maxPagesBeforeCurrentPage;
                    endPage = currentPage + maxPagesAfterCurrentPage;
                }
            }

            // calculate start and end item indexes
            var startIndex = (currentPage - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize - 1, totalItems - 1);

            // create an array of pages that can be looped over
            var pages = Enumerable.Range(startPage, (endPage + 1) - startPage);

            // update object instance with all pager properties required by the view
            var PageInfo = new PageInfo
            {
                TotalNumCrashes = totalItems,
                CrashesPerPage = pageSize,
                CurrentPage = currentPage,
                StartPage = startPage,
                EndPage = endPage,
                StartIndex = startIndex,
                EndIndex = endIndex,
                Pages = pages
            };
            return PageInfo;
        }
        [HttpGet]
        public IActionResult SummaryData(double severity, int pageNum = 1)
        {
            int pageSize = 10;
            int total = 0;
            if (severity != 0)
            {
                total = repo.Crashes.Where(x => x.crash_severity_id == severity).Count();
            }
            else
            {
                total = repo.Crashes.Count();
            }
            var things = new CrashViewModel()
            {
                CarCrashes = repo.Crashes
                    .Where(x => x.crash_severity_id == severity || severity == 0)
                    .AsEnumerable()
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),
                    PageInfo = GetPageInfo(total, pageNum, 10, 10)
                };
            
            return View(things);
        }
        [HttpPost]
        public IActionResult SummaryData(int m = 0, int y = 0, string c = null, string co = null, double s = 0)
        {
            var pageNum = 1;
            var pageSize = 10;

            int month = m;
            int year = y;
            string city = c;
            string county = co;
            double severity = s;

            var monthQuery = from crash in repo.Crashes where crash.crash_date.Month == month select crash;
            var yearQuery = from crash in repo.Crashes where crash.crash_date.Year == year select crash;
            var cityQuery = from crash in repo.Crashes where crash.city == city select crash;
            var countyQuery = from crash in repo.Crashes where crash.county_name == county select crash;
            var severityQuery = from crash in repo.Crashes where crash.crash_severity_id == severity select crash;

            var queryCrash = monthQuery.Concat(yearQuery).Concat(cityQuery).Concat(countyQuery).Concat(severityQuery);

            

            var things = new CrashViewModel()
            {
                CarCrashes = queryCrash
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),
                PageInfo = GetPageInfo(queryCrash.Count(), pageNum, 10, 10)
            };

            ViewBag.Crashes = queryCrash;

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
                var i = repo.Crashes.Count()+8251;
                c.Field1 = i + 1;
                c.crash_id = c.Field1;
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
                return View("SummaryInitial", c);
            }
            else
            {
                return View("EditAdd", c);
            }
        }
        [HttpGet]
        public IActionResult Delete(int fieldId)
        {
            var to_delete = repo.Crashes.Single(x => x.Field1 == fieldId);

            return View("Confirmation", to_delete);
        }

        [HttpPost]
        public IActionResult Delete(CarCrash cc)
        {

            repo.DeleteCrash(cc);

            return RedirectToAction("SummaryData");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
