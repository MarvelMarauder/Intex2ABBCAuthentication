using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2ABBCAuthentication.Models
{
    public class Prediction
    {
        public float PredictedValue { get; set; }
        public CarCrash crash { get; set; }
    }
}
