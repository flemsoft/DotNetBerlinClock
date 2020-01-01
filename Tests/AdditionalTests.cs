using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock.Tests
{
    [TestClass]
    public class AdditionalTests
    {
        private ITimeConverter berlinClock = new TimeConverter();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNull_IfHourOutOfRange()
        {
            berlinClock.convertTime(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowArgument_IfTimeMoreThan24Hours()
        {
            berlinClock.convertTime("24:00:01");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowArgument_IfMinutesOutOfRange()
        {
            berlinClock.convertTime("16:62:42");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowArgument_IfSecondsOutOfRange()
        {
            berlinClock.convertTime("12:00:70");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowArgument_IfTimeWithFractions()
        {
            berlinClock.convertTime("01:03:10:32");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowArgument_IfWrongTimeFormat()
        {
            berlinClock.convertTime("00:6:10");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowArgument_IfNegativeTime()
        {
            berlinClock.convertTime("00:18:-3");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowArgument_IfFakeLeapSecond()
        {
            berlinClock.convertTime("22:59:60");
        }
    }
}
