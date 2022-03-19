using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    public class Remark {
        public int Id { get; set; }
        [Required]
        public PatientFile PatientFile { get; set; }
        public int PatientFileId { get; set; }
        [Required]
        public string RemarkText { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public Employee RemarkBy { get; set; }
        [Required]
        public bool VisibleForPatient { get; set; }
    }
}
