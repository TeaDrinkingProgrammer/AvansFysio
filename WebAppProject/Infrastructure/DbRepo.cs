using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure {
    public abstract class DbRepo<T> : IRepo<T> {
        protected FysioContext _context { get; set; }

        public DbRepo(FysioContext fysioContext) {
            _context = fysioContext;
        }
        abstract public Task<IEnumerable<T>> GetAllAsync();

        public void Add(T item) {
            _context.Add(item);
            _context.SaveChanges();
        }

        abstract public int Count();

        public void Remove(T item) {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public void Update(T item) {
            _context.Update(item);
            _context.SaveChanges();
        }
        abstract public IQueryable<T> Get();
        abstract public T GetWhereId(int id);
    }
}
