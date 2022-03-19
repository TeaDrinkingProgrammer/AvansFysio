using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure {
    public class DbTreatmentPlanRepo : DbRepo<TreatmentPlan>, ITreatmentPlanRepo {
        public DbTreatmentPlanRepo(FysioContext fysioContext) : base(fysioContext) {
        }

        public override int Count() {
            return _context.Patients.Count();
        }

        public override IQueryable<TreatmentPlan> Get() {
            return _context.TreatmentPlans;
        }

        public async override Task<IEnumerable<TreatmentPlan>> GetAllAsync() {
            return await _context.TreatmentPlans.ToListAsync();
        }

        public override TreatmentPlan GetWhereId(int id) {
            return _context.TreatmentPlans.Find(id);
        }
    }
}
