using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2ABBCAuthentication.Models
{
    public interface ICrashRepository
    {
        IQueryable<CarCrash> Crashes { get; }

        public void SaveCrash(CarCrash c);
        public void CreateCrash(CarCrash c);
        public void DeleteCrash(CarCrash c);
    }
}
