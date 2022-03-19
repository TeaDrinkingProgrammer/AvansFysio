using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models {
    public class RemarkViewModel {
        [Required]
        public int PatientFileId { get; set; }
        [Required]
        public string RemarkText { get; set; }
        public Employee MainTherapist { get; set; }
        [Required]
        public bool VisibleForPatient { get; set; }
    }
}
