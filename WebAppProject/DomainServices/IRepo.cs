using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices {
    public interface IRepo<T> {
        public void Add(T item);
        public void Remove(T item);
        public void Update(T item);
        public T GetWhereId(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        /// <summary>Get returns a <see cref="IQueryable"/>, do not forget ToList!</summary>
        public IQueryable<T> Get();
        public int Count();
    }
}
