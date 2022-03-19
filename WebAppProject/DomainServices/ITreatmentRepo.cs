using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainServices {
    public interface ITreatmentRepo : IRepo<Treatment> {
        Treatment GetEmployeeId(int id);
    }
}
