using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models {
    public class AvailabilityViewModel {
        public Availability[] Availibility { get; set; }
        public int EmployeeId { get; set; }
    }
}
