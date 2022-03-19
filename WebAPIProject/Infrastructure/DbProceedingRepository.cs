using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure {
    public class DbProceedingRepository : IProceedingRepo {
        public StamDataContext _context { get; set; }
        public DbProceedingRepository(StamDataContext stamDataContext) {
            _context = stamDataContext;
        }

        public int Count() {
            return _context.Diagnoses.Count();
        }

        public IQueryable<Proceeding> Get() {
            return _context.Proceedings;
        }

        public async Task<IEnumerable<Proceeding>> GetAll() {
            return await _context.Proceedings.OrderBy(x => x.Description).ToListAsync();
        }

        public async Task<Proceeding> GetWhereId(string id) {
            return await _context.Proceedings.FindAsync(id).AsTask();
        }

        public async Task<IEnumerable<Proceeding>> GetWhereDescription(string description) {
            return await _context.Proceedings.Where(x => x.Description.ToLower().Contains(description.ToLower())).ToListAsync();
        }
    }
}
