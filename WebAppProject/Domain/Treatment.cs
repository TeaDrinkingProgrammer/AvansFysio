using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    public class Treatment {
        public int ProceedingCode { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public LocationEnum Room { get; set; }
        public DateTime Date { get; set; }
        public int CarriedOutById { get; set; }
        public Employee CarriedOutBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
