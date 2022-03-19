using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    public class Availability {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int StartHour { get; set; } = 0;
        public int EndHour { get; set; } = 0;
        public bool IsAvailable(DateRange dateRange) {
            if (dateRange != null) {
                return StartHour <= dateRange.Start.Hour && dateRange.End.Hour <= EndHour;
            } else {
                return false;
            }
        }
    }
}
