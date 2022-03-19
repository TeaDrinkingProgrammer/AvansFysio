using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    public class TreatmentPlan {
        [Key]
        public int Id { get; set; }
        public int PatientFileId { get; set; }
        public PatientFile PatientFile { get; set; }
        public TimeSpan SessionDuration { get; set; }
        public int SessionCount { get; set; }
        public string ProceedingCode { get; set; }
    }
}
