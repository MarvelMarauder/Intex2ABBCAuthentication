using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2ABBCAuthentication.Models
{
    public class CrashContext : DbContext
    {
        public CrashContext()
        {
        }

        public CrashContext(DbContextOptions<CrashContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CarCrash> carcrash { get; set; }
    }
}
