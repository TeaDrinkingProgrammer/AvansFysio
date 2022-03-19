using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    public interface IDiagnosisRepo {
        public void Add(Diagnosis diagnosis);
        public void Remove(Diagnosis diagnosis);
        public void Update(Diagnosis diagnosis);
        public Task<Diagnosis> GetWhereId(int id);
        public Task<IEnumerable<Diagnosis>> GetWherePathology(string pathology);
        public Task<IEnumerable<Diagnosis>> GetAll();
        /// <summary>Get returns a <see cref="IQueryable"/>, do not forget ToList!</summary>
        public IQueryable<Diagnosis> Get();
        public int Count();
    }
}
