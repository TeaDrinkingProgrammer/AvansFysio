using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Domain {
    public class Patient : IPerson {
        //If something is changed here, it should also be updated in Detail view
        //TODO add photo
        [Key]
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int RegistrationNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        /// <summary>Must be more than 16 years and cannot lay in the future</summary>
        public DateTime Dob { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string EmailAdress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        //TODO This isn't used when adding patient
        public PatientFile PatientFile { get; set; }
        public int PatientFileId { get; set; }
        public IList<Appointment> Appointments { get; set; }
        public string Image { get; set; }
    }
}
