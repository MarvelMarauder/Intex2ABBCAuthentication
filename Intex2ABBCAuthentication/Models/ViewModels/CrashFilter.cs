using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2ABBCAuthentication.Models.ViewModels
{
    public class CrashFilter
    {
        public string county { get; set; }
        public string city { get; set; }
        public double severity { get; set; }
        public int year { get; set; }
        public int month { get; set; }
    }
}
