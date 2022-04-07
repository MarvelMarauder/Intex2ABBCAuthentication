using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2ABBCAuthentication.Models.ViewModels
{
    public class IntegersUsed
    {
        public ICrashRepository repo { get; set; }
        public int pageSize { get; set; }
        public IntegersUsed(ICrashRepository temp, int page)
        {
            repo = temp;
            pageSize = page;
        }

        public IEnumerable<CarCrash> Crashes2 { get; set; }
        
    }
            
}

