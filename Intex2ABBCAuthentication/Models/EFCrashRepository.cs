using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2ABBCAuthentication.Models
{
    public class EFCrashRepository : ICrashRepository
    {
        private CrashContext context { get; set; }
        public EFCrashRepository(CrashContext temp)
        {
            context = temp;
        }

        public IQueryable<CarCrash> Crashes => context.mytable;

        public void SaveCrash(CarCrash c)
        {
            context.Update(c);
            context.SaveChanges();
        }
        public void CreateCrash(CarCrash c)
        {
            context.Add(c);
            context.SaveChanges();
        }

        public void DeleteCrash(CarCrash p)
        {
            context.Remove(p);
            context.SaveChanges();
        }
    }
}
