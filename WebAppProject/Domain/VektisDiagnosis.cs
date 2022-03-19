using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    [NotMapped]
    public class VektisDiagnosis {
        public int Id { get; set; }
        public string BodyLocalisation { get; set; }
        public string Pathology { get; set; }
        //TODO Not good MVC
        public override string ToString() {
            return BodyLocalisation + " || " + Pathology;
        }
    }
}
