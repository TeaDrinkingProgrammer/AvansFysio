using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DomainServices;
using System.Linq;

namespace Portal.Controllers {
    public class AddPatientController : Controller {
        private readonly ILogger<AddPatientController> _logger;
        private readonly IVektisRepo _vektisRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IPatientFileRepo _patientFileRepo;
        private readonly ITreatmentPlanRepo _treatmentPlanRepo;

        public AddPatientController(ILogger<AddPatientController> logger, IVektisRepo vektisRepo, IPatientRepo patientRepo, IEmployeeRepo employeeRepo, IPatientFileRepo patientFileRepo, ITreatmentPlanRepo treatmentPlanRepo) {
            this._logger = logger;
            _vektisRepo = vektisRepo;
            _patientRepo = patientRepo;
            _employeeRepo = employeeRepo;
            _patientFileRepo = patientFileRepo;
            _treatmentPlanRepo = treatmentPlanRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            //TODO implement proper dropdown, based on the enum
            await HelperMethods.PrefillSelectOptions(_vektisRepo, _employeeRepo, ViewBag);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AddPatientViewModel patient) {
            if (ModelState.IsValid) {
                if (!patient.Image.ContentType.Contains("image/")) {
                    ModelState.AddModelError(nameof(patient.Image), "Het geuploade bestand is geen afbeelding, toegestane formaten zijn: .jpg, .png, .gif en .bmp");
                }
                if (patient.Image.Length > 2000000) {
                    ModelState.AddModelError(nameof(patient.Image), "Afbeelding is te groot. Maximumgrootte is 2MB");
                }
            }
            if (ModelState.IsValid) {
                if (patient.DiagnosisId != -1) {
                    if (_patientRepo.GetWhereEmail(patient.EmailAdress) == null) {
                        patient.DiagnosisDescription = _vektisRepo.GetDiagnosisByIdAsync(patient.DiagnosisId).Result.Pathology;
                        var domainPatient = patient.ToDomain();
                        var patientFile = domainPatient.PatientFile;
                        domainPatient.PatientFile = null;
                        _patientRepo.Add(domainPatient);
                        var returnedItem = _patientRepo.GetWhereEmail(domainPatient.EmailAdress);
                        patientFile.Patient = returnedItem;
                        _patientFileRepo.Add(patientFile);

                        return RedirectToAction("Index", "Employee");
                    } else {
                        ModelState.AddModelError("EmailAdress", "Dit emailadres is al geregistreerd");
                    }
                } else {
                    ModelState.AddModelError("DiagnosisId", "Er is geen diagnosis gekozen");
                }
            }
            await HelperMethods.PrefillSelectOptions(_vektisRepo, _employeeRepo, ViewBag);
            return View(patient);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
