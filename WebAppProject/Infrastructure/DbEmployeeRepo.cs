using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure {
    //: IEmployeeRepository
    public class DbEmployeeRepo : DbRepo<Employee>, IEmployeeRepo {
        //private FysioContext _context { get; set; }

        public DbEmployeeRepo(FysioContext fysioContext) : base(fysioContext) {
        }
        public override async Task<IEnumerable<Employee>> GetAllAsync() {
            return await _context.Employees
                .Include(x => x.Appointments)
                .ToListAsync();
        }

        public override int Count() {
            return _context.Employees.Count();
        }
        public override IQueryable<Employee> Get() {
            return _context.Employees;
        }

        public override Employee GetWhereId(int id) {
            return _context.Employees
                .Include(x => x.Appointments)
                .Include(x => x.Availibility)
                .Include(x => x.Treatments)
                .SingleOrDefault(x => x.Id == id);
        }
        public Employee GetWhereEmail(string email) {
            return _context.Employees
                .Include(x => x.Appointments)
                .Include(x => x.Availibility)
                .Include(x => x.Treatments)
                .SingleOrDefault(x => x.EmailAdress == email);
        }
    }
}
