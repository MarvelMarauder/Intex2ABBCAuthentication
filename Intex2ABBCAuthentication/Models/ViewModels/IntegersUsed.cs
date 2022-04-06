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
        public List<int> IntList { get; set; }

        public PageInfo PageInfo => new PageInfo
        {
            TotalNumCrashes = IntList.Count(),
            CrashesPerPage = pageSize,
            CurrentPage = 1
        };

        public List<object> Crashes => FillList(IntList, pageSize);

        public List<object> FillList(List<int> values, int p)
        {
            int i = 0;
            List<CarCrash> hope = new List<CarCrash>();
            List<object> master = new List<object>();

            int remainder = values.Count() % p;

            for (int s = 1; s<=PageInfo.TotalPages; s++)
            {
                
                foreach (int b in values.GetRange(i, i+p-1))
                {
                    hope.Add(repo.Crashes.Single(x => x.Field1 == b));
                }
                master.Add(hope);
                hope.Clear();
                if (i+p > values.Count()-1)
                {
                    p = remainder-1;
                }
                else
                {
                    i = i + p;
                }
            }

            
            return master;
        }
            
}
}
