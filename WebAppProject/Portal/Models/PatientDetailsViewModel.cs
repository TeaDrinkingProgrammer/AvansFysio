using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models {
    public class PatientDetailsViewModel {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        /// <summary>Must be more than 16 years and cannot lay in the future</summary>
        public DateTime Dob { get; set; }
        public Gender Gender { get; set; }
        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }

        //Patientfile
        public VektisDiagnosis Diagnosis { get; set; }
        public string DiagnosisDescription { get; set; }
        public string Condition { get; set; }
        public KindOfPatient KindOfPatient { get; set; }
        public Employee IntakeBy { get; set; }
        public Employee IntakeUnderSupervisionOf { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public DateTime? DateOfDischarge { get; set; }
        public Employee MainTherapist { get; set; }
        public IEnumerable<Remark> Remarks { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
        public IList<Treatment> Treatments { get; set; }
        //TreatmentPlan
        public TimeSpan SessionDuration { get; set; }
        public int SessionCount { get; set; }
        public string ProceedingCode { get; set; }
    }
}
