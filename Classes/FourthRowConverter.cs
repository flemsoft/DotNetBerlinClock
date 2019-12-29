using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock
{
    class FourthRowConverter : IRowConverter
    {
        const int LampsInQuarter = 3;

        /// <summary>
        /// Fill red lamps in minutes row
        /// </summary>
        public string Convert(string row)
        {
            var result = new StringBuilder(row);

            for (int i = 0; i < result.Length; i++)
            {
                if ((i + 1) % LampsInQuarter == 0)
                    result[i] = (char)LampState.Red;
            }

            return result.ToString();
        }
    }
}
