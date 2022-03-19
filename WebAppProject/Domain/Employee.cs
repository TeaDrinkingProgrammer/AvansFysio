
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain {
    public class Employee : IPerson {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string EmailAdress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int RegistrationNumber { get; set; }
        public bool IsTherapist => BigNumber != null;
        public int? BigNumber { get; set; }
        public IList<Appointment> Appointments { get; set; }
        public IList<Availability> Availibility { get; set; }
        public IList<Treatment> Treatments { get; set; }

        public bool IsAvailable(DateRange dateRange) {
            if (Availibility[(int)dateRange.Start.DayOfWeek] != null) {
                if (Availibility[(int)dateRange.Start.DayOfWeek].IsAvailable(dateRange)) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }
    }
}
