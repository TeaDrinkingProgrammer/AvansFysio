using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure {
    public class DbPatientRepo : DbRepo<Patient>, IPatientRepo {
        public DbPatientRepo(FysioContext fysioContext) : base(fysioContext) {
        }

        public override int Count() {
            return _context.Patients.Count();
        }

        public override IQueryable<Patient> Get() {
            return _context.Patients;
        }

        public async override Task<IEnumerable<Patient>> GetAllAsync() {
            return await _context.Patients.Include(x => x.PatientFile).ThenInclude(x => x.IntakeBy)
                .Include(x => x.PatientFile).ThenInclude(x => x.IntakeUnderSupervisionOf)
                .Include(x => x.PatientFile).ThenInclude(x => x.MainTherapist)
                .Include(x => x.PatientFile).ThenInclude(x => x.TreatmentPlan)
                .ToListAsync();
        }

        public override Patient GetWhereId(int id) {
            return _context.Patients
                .Include(x => x.Appointments)
                .Include(x => x.PatientFile).ThenInclude(x => x.IntakeBy)
                .Include(x => x.PatientFile).ThenInclude(x => x.IntakeUnderSupervisionOf)
                .Include(x => x.PatientFile).ThenInclude(x => x.MainTherapist)
                .Include(x => x.PatientFile).ThenInclude(x => x.TreatmentPlan)
                .Include(x => x.PatientFile).ThenInclude(x => x.Treatments).ThenInclude(x => x.CarriedOutBy)
                .FirstOrDefault(x => x.PatientId == id);
        }
        public Patient GetWhereEmail(string email) {
            return _context.Patients
                .Include(x => x.Appointments).ThenInclude(x => x.Employee)
                .Include(x => x.PatientFile).ThenInclude(x => x.IntakeBy)
                .Include(x => x.PatientFile).ThenInclude(x => x.IntakeUnderSupervisionOf)
                .Include(x => x.PatientFile).ThenInclude(x => x.MainTherapist)
                .Include(x => x.PatientFile).ThenInclude(x => x.TreatmentPlan)
                .Include(x => x.PatientFile).ThenInclude(x => x.Remarks)
                .Include(x => x.PatientFile).ThenInclude(x => x.Treatments).ThenInclude(x => x.CarriedOutBy)
                .FirstOrDefault(x => x.EmailAdress == email);
        }

        public void UpdateExcludingPatientFile(Patient patient) {
            _context.Patients.Update(patient);
            _context.Entry(patient).Property(x => x.PatientFileId).IsModified = false;
            _context.SaveChanges();
        }
    }
}
