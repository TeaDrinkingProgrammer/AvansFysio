using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models {
    public class EmployeeHomeViewModel {
        public IEnumerable<PatientInfoViewModel> Patients { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
        public IEnumerable<Treatment> Treatments { get; set; }
        public int Id { get; set; }
    }
}
