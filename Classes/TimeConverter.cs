using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        const int MaxHourValue = 24;
        const int MinutesInOneHour = 60;
        const int SecondsInOneMinute = 60;

        const int ClockStep = 5;

        const int TotalLampsInMinutesRow = MinutesInOneHour / ClockStep;

        public string convertTime(string aTime)
        {
            if (aTime == null)
                throw new ArgumentNullException(nameof(aTime));

            // Parse time string
            var timeParts = aTime.Split(':');
            if (timeParts.Length != 3)
                throw new ArgumentException(Properties.Resources.InvalidTimeFormatMessage);

            int hours;
            int minutes;
            int seconds;
            if (!ParseTimePart(timeParts[0], MaxHourValue, out hours) ||
                !ParseTimePart(timeParts[1], MinutesInOneHour - 1, out minutes) ||
                !ParseTimePart(timeParts[2], SecondsInOneMinute - 1, out seconds))
                throw new ArgumentException(Properties.Resources.InvalidTimeFormatMessage);

            if (hours == MaxHourValue && (minutes > 0 || seconds > 0))
                throw new ArgumentException(Properties.Resources.InvalidTimeFormatMessage);

            var result = new StringBuilder();

            // First (seconds) row
            var secondsLampState = seconds % 2 == 0 ? LampState.Yellow : LampState.Off;
            result.AppendLine(((char)secondsLampState).ToString());

            // Second (hours) row
            var hoursFirstRowCount = hours / ClockStep;
            result.AppendLine(GetRowWithLamps(hoursFirstRowCount, ClockStep - 1, LampState.Red));

            // Third (hours) row
            var hoursSecondRowCount = hours % ClockStep;
            result.AppendLine(GetRowWithLamps(hoursSecondRowCount, ClockStep - 1, LampState.Red));

            // Fourth (minutes) row
            Debug.Assert(TotalLampsInMinutesRow == 12);
            var minutesFirstRowCount = minutes / ClockStep;
            result.AppendLine(GetRowWithLamps(minutesFirstRowCount, TotalLampsInMinutesRow - 1, LampState.Yellow, new FourthRowConverter()));

            // Fifth (minutes) row
            var minutesSecondRowCount = minutes % ClockStep;
            result.Append(GetRowWithLamps(minutesSecondRowCount, ClockStep - 1, LampState.Yellow));

            return result.ToString();
        }

        bool ParseTimePart(string part, int maxValue, out int value)
        {
            if (part.Length != 2)
            {
                value = default(int);
                return false;
            }

            if (!int.TryParse(part, out value))
                return false;

            return 0 <= value && value <= maxValue;
        }

        string GetRowWithLamps(int count, int totalLampsInRow, LampState lampOnState, IRowConverter rowConverter = null)
        {
            Debug.Assert(count <= totalLampsInRow);
            Debug.Assert(lampOnState != LampState.Off);

            var row = new string((char)lampOnState, count);

            if (rowConverter != null)
                row = rowConverter.Convert(row);

            row = row.PadRight(totalLampsInRow, (char)LampState.Off);
            return row;
        }
    }
}
