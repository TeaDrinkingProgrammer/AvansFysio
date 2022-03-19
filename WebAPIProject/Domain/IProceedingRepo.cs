using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    public interface IProceedingRepo {
        public Task<Proceeding> GetWhereId(string id);
        public Task<IEnumerable<Proceeding>> GetWhereDescription(string description);
        public Task<IEnumerable<Proceeding>> GetAll();
        /// <summary>Get returns a <see cref="IQueryable"/>, do not forget ToList!</summary>
        public int Count();
    }
}
