using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2ABBCAuthentication.Components
{
    public class FilterViewComponent : ViewComponent
    {
        public FilterViewComponent()
        {
        }
        public async  Task<IViewComponentResult> InvokeAsyn()
        {
            return View();
        }
    }
}
