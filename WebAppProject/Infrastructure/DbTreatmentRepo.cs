using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure {
    //: IEmployeeRepository
    public class DbTreatmentRepo : DbRepo<Treatment>, ITreatmentRepo {
        //private FysioContext _context { get; set; }

        public DbTreatmentRepo(FysioContext fysioContext) : base(fysioContext) {
        }
        public override async Task<IEnumerable<Treatment>> GetAllAsync() {
            return await _context.Treatments
                .Include(x => x.CarriedOutBy)
                .ToListAsync();
        }

        public override int Count() {
            return _context.Treatments.Count();
        }
        public override IQueryable<Treatment> Get() {
            return _context.Treatments;
        }

        public override Treatment GetWhereId(int id) {
            return _context.Treatments
                .Include(x => x.CarriedOutBy)
                .SingleOrDefault(x => x.Id == id);
        }

        public Treatment GetEmployeeId(int id) {
            return _context.Treatments
                .Include(x => x.CarriedOutBy)
                .SingleOrDefault(x => x.CarriedOutBy.Id == id);
        }
    }
}
