using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2ABBCAuthentication.Models
{
    public class DbSecret
    {
        public static string GetRDSConnectionString(string dbname = "sys")
        {
            if (string.IsNullOrEmpty(dbname)) return null;
            string identity = Environment.GetEnvironmentVariable("RDS_USERS");
            string crashes = Environment.GetEnvironmentVariable("RDS_CRASHES");
            if (dbname == "first")
            {
                dbname = identity;
            }
            else
            {
                dbname = crashes;
            }
            
            string server = Environment.GetEnvironmentVariable("RDS_SERVER");
            string port = Environment.GetEnvironmentVariable("RDS_PORT");
            string user = Environment.GetEnvironmentVariable("RDS_USER");
            string password = Environment.GetEnvironmentVariable("RDS_PASSWORD");
            return "server=" + server + ";port=" + port + ";database=" + dbname + ";user=" + user + ";password=" + password;
        }
    }
}
