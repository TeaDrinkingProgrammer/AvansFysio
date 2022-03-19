using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure {
    public class DbDiagnosisRepository : IDiagnosisRepo {
        public StamDataContext _context { get; set; }
        public DbDiagnosisRepository(StamDataContext stamDataContext) {
            _context = stamDataContext;
        }
        public void Add(Diagnosis diagnosis) {
            _context.Add(diagnosis);
            _context.SaveChanges();
        }

        public int Count() {
            return _context.Diagnoses.Count();
        }

        public IQueryable<Diagnosis> Get() {
            return _context.Diagnoses;
        }

        public async Task<IEnumerable<Diagnosis>> GetAll() {
            return await _context.Diagnoses.OrderBy(x => x.BodyLocalisation).ToListAsync();
        }

        public async Task<Diagnosis> GetWhereId(int id) {
            return await _context.Diagnoses.FindAsync(id).AsTask();
        }

        public void Remove(Diagnosis diagnosis) {
            _context.Remove(diagnosis);
            _context.SaveChanges();
        }

        public void Update(Diagnosis diagnosis) {
            _context.Update(diagnosis);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Diagnosis>> GetWherePathology(string pathology) {
            return await _context.Diagnoses.Where(x => x.Pathology.ToLower().Contains(pathology.ToLower())).ToListAsync();
        }
    }
}
