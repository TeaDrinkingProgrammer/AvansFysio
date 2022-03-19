using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainServices {
    public interface IPatientRepo : IRepo<Patient> {
        Patient GetWhereEmail(string email);
        void UpdateExcludingPatientFile(Patient patient);
    }
}
