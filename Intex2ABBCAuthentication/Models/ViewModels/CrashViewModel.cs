using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2ABBCAuthentication.Models.ViewModels
{
    public class CrashViewModel
    {
        public IEnumerable<CarCrash> CarCrashes { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
