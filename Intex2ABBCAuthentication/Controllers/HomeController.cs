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

        //public IActionResult SummaryData(int pageNum = 1)
        //{
        //    int pageSize = 100;

        //    var x = new CrashViewModel
        //    {
        //        //CarCrashes = repo.Crashes
        //        //.Where(b => b.Category == category || category == null)
        //        //.Skip((pageNum - 1) * pageSize)
        //        //.Take(pageSize),

        //        //Link Bullocks form here

        //        //PageInfo = new PageInfo
        //        //{
        //        //    TotalNumCrashes =
        //        //        (category == null
        //        //        ? repo.Crashes.Count()
        //        //        : repo.Crashes.Where(x => x.Category == category).Count()),
        //        //    CrashesPerPage = pageSize,
        //        //    CurrentPage = pageNum
        //        //}

        //    };
        //    return View();
        //}
        [HttpGet]
        public IActionResult SummaryInitial()
        {

            return View();
        }
        [HttpPost]
        public IActionResult SummaryInitial(CrashFilter c)
        {
            
            int month = c.month;
            int year = c.year;
            string city = c.city;
            string county = c.county;
            double severity = c.severity;

            MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;database=intexcrashes;user=root;password=usingwindowsisgr8");
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select * from mytable where year(crash_date) = @year && month(crash_date) = @month " +
                "&& county_name = @county && city = @city && crash_severity_id = @crash";
            command.Parameters.AddWithValue("@year", year);
            command.Parameters.AddWithValue("@month", month);
            command.Parameters.AddWithValue("@county", county);
            command.Parameters.AddWithValue("@crash", severity);
            command.Parameters.AddWithValue("@city", city);

            MySqlDataReader stuff = command.ExecuteReader();

            List<int> x = new List<int>();


            while (stuff.Read())
            {
                x.Add(stuff.GetInt32(stuff.GetOrdinal("Field1")));
            }


            connection.Close();



            return View(x);
            
        }
        [HttpPost]
        public IActionResult SummaryData(CrashFilter c)
        {
            int month = c.month;
            int year = c.year;
            string city = c.city;
            string county = c.county;
            double severity = c.severity;

            MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;database=intexcrashes;user=root;password=usingwindowsisgr8");
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select * from mytable where year(crash_date) = @year && month(crash_date) = @month " +
                "&& county_name = @county && city = @city && crash_severity_id = @crash";
            command.Parameters.AddWithValue("@year", year);
            command.Parameters.AddWithValue("@month", month);
            command.Parameters.AddWithValue("@county", county);
            command.Parameters.AddWithValue("@crash", severity);
            command.Parameters.AddWithValue("@city", city);

            MySqlDataReader stuff = command.ExecuteReader();

            List<int> x = new List<int>();


            while (stuff.Read())
            {
                x.Add(stuff.GetInt32(stuff.GetOrdinal("Field1")));
            }


            connection.Close();

            var things = new IntegersUsed(repo, 100)
            {
                IntList = x
            };

            return View(things);

        }
        [HttpGet]
        public IActionResult SummaryData(int pageNum = 1)
        {
            int pageSize = 100;
            return View();
        }

        [HttpGet]
        public IActionResult Details(int fieldid)
        {
            var blah = repo.Crashes.Single(x => x.Field1 == fieldid);
            return View(blah);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
