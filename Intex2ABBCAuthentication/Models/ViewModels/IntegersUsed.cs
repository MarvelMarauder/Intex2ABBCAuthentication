﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2ABBCAuthentication.Models.ViewModels
{
    public class IntegersUsed
    {
        public ICrashRepository repo { get; set; }
        public IntegersUsed(ICrashRepository temp)
        {
            repo = temp;
        }
        public List<int> IntList { get; set; }
        public List<CarCrash> Crashes => FillList(IntList);
        
        public List<CarCrash> FillList(List<int> values)
        {
            List<CarCrash> hope = new List<CarCrash>();
            foreach (int i in values)
            {
                hope.Add(repo.Crashes.Single(x => x.Field1 == i));
            }
            return hope;
        }
            
}
}
