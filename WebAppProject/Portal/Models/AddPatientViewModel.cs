using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain;
using Portal.Attributes;
using Microsoft.AspNetCore.Http;

namespace Portal.Models {
    public class AddPatientViewModel {
        [Required(ErrorMessage = "Vul AUB een naam in")]
        public string Name { get; set; }
        ///<summary>At least 16 years and not in the future</summary>
        [Required(ErrorMessage = "Vul AUB een geldige geboortedatum in")]
        [MinimumAge(16, ErrorMessage = "U moet minimaal 16 jaar oud zijn")]
        [NotInFuture(ErrorMessage = "Geboortedatum mag niet in de toekomst liggen")]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "Vul AUB een Registratienummer in")]
        public int RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Kies een geslacht")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Vul AUB een geldig Emailadres in")]
        [EmailAddress]
        public string EmailAdress { get; set; }
        [Required(ErrorMessage = "Vul AUB een geldig Telefoonnummer in")]
        [Phone]
        public string PhoneNumber { get; set; }
        //Patientdossier
        //Age is calculated
        [Required(ErrorMessage = "Kies een diagnosis")]
        public int DiagnosisId { get; set; }
        public string DiagnosisDescription { get; set; }
        [Required(ErrorMessage = "Vul AUB een beschrijving van de klachten in")]
        public string Condition { get; set; }
        [Required(ErrorMessage = "Kies een soort patient")]
        public KindOfPatient KindOfPatient { get; set; }
        [Required(ErrorMessage = "Kies een medewerker door wie de intake is gedaan")]
        public int IntakeById { get; set; }
        public int? IntakeUnderSupervisionOfId { get; set; }
        [Required(ErrorMessage = "Kies een aanmelddatum")]
        public DateTime DateOfAdmission { get; set; }
        [Required(ErrorMessage = "Kies een hoofdbehandelaar")]
        public int MainTherapistId { get; set; }


        //Behandelplan
        [Required(ErrorMessage = "Kies een sessieduur")]
        public int SessionDuration { get; set; }
        [Required(ErrorMessage = "Kies een sessieduur")]
        public int SessionCount { get; set; }
        [Required(ErrorMessage = "Kies een behandelcode")]
        public string ProceedingCode { get; set; }
        [Required(ErrorMessage = "Upload a.u.b een foto")]
        public IFormFile Image { get; set; }
    }
}
