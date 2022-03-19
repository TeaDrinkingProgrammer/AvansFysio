using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    [NotMapped]
    public class VektisProceeding {
        public string Id { get; set; }
        public string Description { get; set; }
        public string ExplanationRequired { get; set; }
        //TODO Not good MVC
        public override string ToString() {
            return Description + " || " + ExplanationRequired;
        }
    }
}
