using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices {
    public interface IVektisRepo {
        public Task<IEnumerable<VektisDiagnosis>> GetAllDiagnosesAsync();
        public Task<VektisDiagnosis> GetDiagnosisByIdAsync(int id);
        public Task<IEnumerable<VektisProceeding>> GetAllProceedingsAsync();
        public Task<VektisProceeding> GetVektisProceedingByIdAsync(int id);
        public int Count();
    }
}
