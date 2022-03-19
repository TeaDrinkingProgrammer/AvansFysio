using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models {
    public class HelperMethods {
        public static async Task<dynamic> PrefillSelectOptions(IVektisRepo vektisRepo, IEmployeeRepo employeeRepo, dynamic ViewBag) {
            var diagnoses = await vektisRepo.GetAllDiagnosesAsync();
            var proceedings = await vektisRepo.GetAllProceedingsAsync();
            var employees = await employeeRepo.GetAllAsync();
            ViewBag.Employees = employees;
            ViewBag.Therapists = employees.Where(e => e.IsTherapist).ToList();
            ViewBag.Diagnoses = diagnoses;
            ViewBag.Proceedings = proceedings;
            return ViewBag;
        }
    }
}
