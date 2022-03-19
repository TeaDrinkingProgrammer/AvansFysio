using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models {
    public class RegisterModel {
        [Required(ErrorMessage = "Vul AUB een (geldig) emailadres in")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vul AUB een wachtwoord in")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vul AUB een tweede keer uw wachtwoord in")]
        public string PasswordCheck { get; set; }
    }
}
