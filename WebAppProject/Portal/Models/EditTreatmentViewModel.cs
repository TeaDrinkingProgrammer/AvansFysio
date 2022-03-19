using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models {
    public class EditTreatmentViewModel {
        [Required(ErrorMessage = "Kies aub een behandeling")]
        public int ProceedingCode { get; set; }
        public int Id { get; set; }
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Kies aub een locatie")]
        public LocationEnum Room { get; set; }
        public string TreatmentRemarks { get; set; }
        [Required(ErrorMessage = "Kies aub een datum")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Kies aub een behandelaar")]
        public int CarriedOutById { get; set; }
        public DateTime CreatedOn { get; } = DateTime.Now;
        public bool VisibleForPatient { get; set; }
    }
}
