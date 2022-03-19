using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Portal.Query {
    public class Query {
        #region Constructor  
        public Query(IDiagnosisRepo diagnosisRepo, IProceedingRepo proceedingRepo) {
            _diagnosisRepo = diagnosisRepo;
            _proceedingRepo = proceedingRepo;

        }
        #endregion

        #region Property  
        private readonly IDiagnosisRepo _diagnosisRepo;
        private readonly IProceedingRepo _proceedingRepo;
        #endregion

        public async Task<Diagnosis> DiagnosisWhereId(int code) => await _diagnosisRepo.GetWhereId(code);
        public async Task<IEnumerable<Diagnosis>> Diagnoses() => await _diagnosisRepo.GetAll();
        public async Task<Proceeding> ProceedingWhereId(string value) => await _proceedingRepo.GetWhereId(value);
        public async Task<IEnumerable<Proceeding>> Proceedings() => await _proceedingRepo.GetAll();
    }
}
