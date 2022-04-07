using Intex2ABBCAuthentication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2ABBCAuthentication.Components
{
    public class FilterViewComponent : ViewComponent
    {
        private ICrashRepository repo { get; set; }

        public FilterViewComponent(ICrashRepository temp)
        {
            repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["severity"];
            var categories = repo.Crashes
                .Select(x => x.crash_severity_id)
                .Distinct()
                .OrderBy(x => x);

            return View(categories);
        }
    }
}
