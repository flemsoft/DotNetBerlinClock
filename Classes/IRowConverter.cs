using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock
{
    interface IRowConverter
    {
        string Convert(string row);
    }
}
