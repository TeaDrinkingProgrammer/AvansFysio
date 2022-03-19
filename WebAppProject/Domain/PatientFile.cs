using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    public class PatientFile {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        [Required]
        public int DiagnosisId { get; set; }
        [Required]
        public string DiagnosisDescription { get; set; }
        [Required]
        public string Condition { get; set; }
        [Required]
        public KindOfPatient KindOfPatient { get; set; }
        public Employee IntakeBy { get; set; }
        public int IntakeById { get; set; }
        public Employee IntakeUnderSupervisionOf { get; set; }
        public int? IntakeUnderSupervisionOfId { get; set; }
        [Required]
        public DateTime DateOfAdmission { get; set; }
        public DateTime? DateOfDischarge { get; set; }
        public Employee MainTherapist { get; set; }
        public int MainTherapistId { get; set; }
        public Collection<Remark> Remarks { get; set; }
        [Required]
        public TreatmentPlan TreatmentPlan { get; set; }
        public int TreatmentPlanId { get; set; }
        public IList<Treatment> Treatments {get;set;}
        public int GetAge() {
            return DateTime.Today.Year - Patient.Dob.Year;
        }
    }
}
