using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models {
    public class EditAppointmentViewModel {
        public int? Id { get; set; }
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Vul AUB een datum in")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Vul AUB een starttijd in")]
        public TimeSpan StartTime { get; set; }
        public TimeSpan SessionDuration { get; set; }
        public int PatientId { get; set; }
    }
}
