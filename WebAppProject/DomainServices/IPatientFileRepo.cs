using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices {
    public interface IPatientFileRepo : IRepo<PatientFile> {
        public PatientFile GetWherePatientId(int patientId);
    }
}
