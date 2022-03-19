using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure {
    public class DbPatientFileRepo : DbRepo<PatientFile>, IPatientFileRepo {
        public DbPatientFileRepo(FysioContext fysioContext) : base(fysioContext) {
        }

        public override int Count() {
            return _context.Patients.Count();
        }
        public override IQueryable<PatientFile> Get() {
            return _context.PatientFiles;
        }

        public async override Task<IEnumerable<PatientFile>> GetAllAsync() {
            return await _context.PatientFiles.ToListAsync();
        }

        public override PatientFile GetWhereId(int id) {
            return _context.PatientFiles
                .Include(x => x.Remarks)
                .Include(x => x.MainTherapist)
                .Include(x => x.Treatments).ThenInclude(x => x.CarriedOutBy)
                .SingleOrDefault(x => x.Id == id);
        }

        public PatientFile GetWherePatientId(int patientId) {
            return _context.PatientFiles
                .Include(x => x.Remarks)
                .Include(x => x.MainTherapist)
                .Include(x => x.Treatments).ThenInclude(x => x.CarriedOutBy)
                .SingleOrDefault(x => x.PatientId == patientId);
        }
    }
}
