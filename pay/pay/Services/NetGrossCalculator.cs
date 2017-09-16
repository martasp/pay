using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pay.Services
{
    public static class NetGrossCalculator
    {
        public static float CalculateGross(double net)
        {
            double groos = double.NaN;
            if (net<282.1)
            {
                groos = net / 0.91;
            }
            else if (net < 335.3)
            {
                groos = (net - 46.5) / 0.76;
            }
            else if (net < 760)
            {
                groos = (net - 75) / 0.685;
            }
            else if (net >= 760)
            {
                groos = net / 0.76;
            }
            return (float)Math.Round(groos, 2);
        } 
    }
}
