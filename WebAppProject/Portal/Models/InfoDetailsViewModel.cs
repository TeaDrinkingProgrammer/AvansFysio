using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models {
    public class InfoDetailsViewModel {
        public int Id { get; set; }
        public int RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        /// <summary>Must be more than 16 years and cannot lay in the future</summary>
        public DateTime Dob { get; set; }
        public Gender Gender { get; set; }
        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
