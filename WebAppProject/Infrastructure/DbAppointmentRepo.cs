using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure {
    public class DbAppointmentRepo : DbRepo<Appointment>, IAppointmentRepo {

        public DbAppointmentRepo(FysioContext fysioContext) : base(fysioContext) {
        }
        public async override Task<IEnumerable<Appointment>> GetAllAsync() {
            return await _context.Appointments.ToListAsync();
        }

        public override int Count() {
            return _context.Appointments.Count();
        }
        public override IQueryable<Appointment> Get() {
            return _context.Appointments;
        }

        public override Appointment GetWhereId(int id) {
            return _context.Appointments
                .SingleOrDefault(x => x.Id == id);
        }

    }
}
