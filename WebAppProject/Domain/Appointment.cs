using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    public class Appointment {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateRange DateRange { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public override string ToString() {
            return $"firstParty: {Employee.Name}, secondParty: {Patient.Name}, startTime: {DateRange.Start.ToString("dd-MM-yyyy HH:mm:ss")} endTime: {DateRange.End.ToString("dd-MM-yyyy HH:mm:ss")}";
        }
    }
}
