using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Domain {
    public interface IPerson {
        [Required]
        public string Name { get; set; }
        [Required]
        public string EmailAdress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int RegistrationNumber { get; set; }
    }
}
