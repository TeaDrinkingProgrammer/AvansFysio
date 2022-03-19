using System.ComponentModel.DataAnnotations;
using Domain;

namespace Portal.Models {
    public class PatientInfoViewModel {
        public int PatientId { get; set; }
        [Required]
        public string Name { get; set; }
        //Min 16 en niet in toekomst
        [Required]
        public string Dob { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAdress { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public KindOfPatient KindOfPatient { get; set; }
    }
}
