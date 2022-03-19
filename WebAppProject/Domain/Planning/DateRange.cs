using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    public class DateRange {
        public DateRange(DateTime start, DateTime end) {
            Start = start;
            End = end;
        }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        /// <summary>
        /// Returns true when a date falls within the bounds of the DateRange
        /// </summary>
        /// <param name="value"></param>
        public bool Includes(DateTime value) {
            return (Start <= value) && (value <= End);
        }

        /// <summary>
        /// Returns true when a inputted DateRange falls within the bounds of the DateRange.
        /// </summary>
        /// <param name="value"></param>
        public bool Includes(DateRange range) {
            return (Start <= range.Start) && (range.End <= End);
        }

        public TimeSpan ToTimeSpan() {
            return End - Start;
        }
    }
}
