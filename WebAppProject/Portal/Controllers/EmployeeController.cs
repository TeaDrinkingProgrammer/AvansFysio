using AutoMapper;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NToastNotify;

namespace Portal.Controllers {
    [Authorize(Policy = "RequireEmployee")]
    public class EmployeeController : Controller {
        private IPatientRepo _patientRepo;
        private IPatientFileRepo _patientFileRepo;
        private IVektisRepo _vektisRepo;
        private IdentityUser CurrentUser;
        private IMapper _mapper;
        private IEmployeeRepo _employeeRepo;
        private ITreatmentPlanRepo _treatmentPlanRepo;
        private IAppointmentRepo _appointmentRepo;
        private IToastNotification _toastNotification;
        private UserManager<IdentityUser> _userMgr;
        private ITreatmentRepo _treatmentRepo;
        public EmployeeController(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userMgr,
            IPatientRepo patientRepo, IPatientFileRepo patientFileRepo,
            IVektisRepo vektisRepo, IMapper mapper, IEmployeeRepo employeeRepo,
            ITreatmentPlanRepo treatmentPlanRepo, IAppointmentRepo appointmentRepo,
            ITreatmentRepo treatmentRepo, IToastNotification toastNotification) {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            CurrentUser = userMgr.FindByIdAsync(userId).Result;
            _patientRepo = patientRepo;
            _patientFileRepo = patientFileRepo;
            _vektisRepo = vektisRepo;
            _mapper = mapper;
            _employeeRepo = employeeRepo;
            _treatmentPlanRepo = treatmentPlanRepo;
            _appointmentRepo = appointmentRepo;
            _toastNotification = toastNotification;
            _userMgr = userMgr;
            _treatmentRepo = treatmentRepo;
        }
        public IActionResult Index() {
            var patients = _patientRepo.GetAllAsync().Result.ToPatientInfoViewModel();
            var currentEmployee = _employeeRepo.GetWhereEmail(CurrentUser.Email);
            EmployeeHomeViewModel model = new() {
                Patients = patients,
                Id = currentEmployee.Id,
                Appointments = currentEmployee.Appointments,
                Treatments = currentEmployee.Treatments
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> PatientDetailsAsync(int id) {
            Patient patient = _patientRepo.GetWhereId(id);
            var model = _mapper.Map<PatientDetailsViewModel>(patient);
            model.Image = "data:image;base64," + patient.Image;
            model.Diagnosis = await _vektisRepo.GetDiagnosisByIdAsync(patient.PatientFile.DiagnosisId);
            ViewBag.cultureInfo = new CultureInfo("nl-NL");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditPatientAsync(int id) {
            Patient patient = _patientRepo.GetWhereId(id);
            var model = _mapper.Map<PatientEditViewModel>(patient);
            await HelperMethods.PrefillSelectOptions(_vektisRepo, _employeeRepo, ViewBag);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPatientAsync(PatientEditViewModel model) {
            if (ModelState.IsValid) {
                if (model.DiagnosisId != -1) {
                    model.DiagnosisDescription = _vektisRepo.GetDiagnosisByIdAsync(model.DiagnosisId).Result.Pathology;
                    if (model.IntakeUnderSupervisionOfId == -1) model.IntakeUnderSupervisionOfId = null;
                    var patient = _mapper.Map<Patient>(model);
                    _patientRepo.Update(patient);

                    return RedirectToAction("Index", "Employee");
                } else {
                    ModelState.AddModelError("DiagnosisId", "Er is geen diagnosis gekozen");
                }
            }
            await HelperMethods.PrefillSelectOptions(_vektisRepo, _employeeRepo, ViewBag);
            return View(model);
        }
        [HttpGet]
        public IActionResult EditAvailability(int id) {
            var employee = _employeeRepo.GetWhereId(id);
            //TODO test if this works
            if (employee.Availibility.Count == 0) {
                for (int i = 0; i < 8; i++) {
                    employee.Availibility.Add(new() { StartHour = 0, EndHour = 0 });
                }
            }
            var model = new AvailabilityViewModel {
                Availibility = employee.Availibility.ToArray(),
                EmployeeId = id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAvailability(AvailabilityViewModel model) {

            if (ModelState.IsValid) {
                var employee = _employeeRepo.GetWhereId(model.EmployeeId);
                employee.Availibility = model.Availibility;
                employee.Availibility = employee.Availibility.ToList();
                _employeeRepo.Update(employee);
                return RedirectToAction("Index", "Employee");
            }
            return View("EditAvailability", model);

        }
        [HttpGet]
        public async Task<IActionResult> EditTreatmentAsync(int id, int patientId) {
            var proceedings = await _vektisRepo.GetAllProceedingsAsync();
            ViewBag.Proceedings = proceedings;
            var employees = await _employeeRepo.GetAllAsync();
            ViewBag.Employees = employees;
            EditTreatmentViewModel model;
            if (id != -1) {
                var treatment = _treatmentRepo.GetWhereId(id);
                model = _mapper.Map<EditTreatmentViewModel>(treatment);
            } else {
                model = new() {
                    Id = -1,
                    Date = DateTime.Now,
                    PatientId = patientId
                };
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTreatmentAsync(EditTreatmentViewModel model) {
            if (ModelState.IsValid) {
                string explanationString = _vektisRepo.GetVektisProceedingByIdAsync(model.ProceedingCode).Result.ExplanationRequired;
                bool explanationRequired = explanationString == "Ja" ? true : false;
                var patientFile = _patientFileRepo.GetWherePatientId(model.PatientId);

                if (patientFile.DateOfAdmission <= model.Date && (patientFile.DateOfDischarge == null ? true : model.Date <= patientFile.DateOfDischarge)) {
                    if (!(explanationRequired && (model.TreatmentRemarks?.Trim().Length == 0 || model.TreatmentRemarks?.Trim().Length == null))) {
                        var treatment = _mapper.Map<Treatment>(model);
                        var employee = _employeeRepo.GetWhereId(model.CarriedOutById);
                        if (model.Id == -1) {
                            treatment.Id = 0;
                            Remark remark = new() {
                                RemarkText = model.TreatmentRemarks,
                                VisibleForPatient = model.VisibleForPatient,
                                Date = model.Date,
                                RemarkBy = employee
                            };
                            patientFile.Remarks.Add(remark);
                            patientFile.Treatments.Add(treatment);
                            _patientFileRepo.Update(patientFile);
                            return RedirectToAction("Index", "Employee");
                        } else {
                            if (DateTime.Now <= (model.CreatedOn + new TimeSpan(24, 00, 00))) {
                                Remark remark = new() {
                                    RemarkText = model.TreatmentRemarks,
                                    VisibleForPatient = model.VisibleForPatient,
                                    Date = model.Date.Date,
                                    RemarkBy = employee
                                };
                                patientFile.Remarks.Add(remark);
                                _patientFileRepo.Update(patientFile);
                                _treatmentRepo.Update(treatment);
                                return RedirectToAction("Index", "Employee");
                            } else {
                                ModelState.AddModelError("Date", "Kan behandeling na maximaal 24 uur wijzigen");
                            }
                        }
                    } else {
                        ModelState.AddModelError("TreatmentRemarks", "Bijzonderheden moeten ingevuld worden");
                    }
                } else {
                    ModelState.AddModelError("Date", "Datum moet tussen binnenkomst en ontslagdatum liggen");
                }
            }
            var proceedings = await _vektisRepo.GetAllProceedingsAsync();
            ViewBag.Proceedings = proceedings;
            var employees = await _employeeRepo.GetAllAsync();
            ViewBag.Employees = employees;
            return View(model);
        }
        [HttpGet]
        public IActionResult NewAppointment(int id) {
            return RedirectToAction("EditAppointment", new { id = -1, patientId = id });
        }

        [HttpGet]
        public async Task<IActionResult> EditAppointmentAsync(int id, int? patientId) {
            var employees = await _employeeRepo.GetAllAsync();
            var claims = await _userMgr.GetClaimsAsync(CurrentUser);
            if (patientId != null) {
                //New appointment
                var patient = _patientRepo.GetWhereId((int)patientId);
                var treatmentPlan = _treatmentPlanRepo.GetWhereId(patient.PatientFile.TreatmentPlanId);
                var model = new EditAppointmentViewModel {
                    SessionDuration = treatmentPlan.SessionDuration,
                    Date = DateTime.Now,
                    StartTime = new TimeSpan(14, 00, 00),
                    Id = -1
                };
                ViewBag.Employees = PreFillOptions(claims, employees);
                return View(model);
            } else {
                ViewBag.Employees = PreFillOptions(claims, employees);
                var appointment = _appointmentRepo.GetWhereId(id);
                return View(appointment.ToViewModel(appointment.DateRange.End - appointment.DateRange.Start));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAppointmentAsync(EditAppointmentViewModel model) {
            var patient = _patientRepo.GetWhereId(model.PatientId);
            if (ModelState.IsValid) {
                TreatmentPlan treatmentPlan = _treatmentPlanRepo.GetWhereId(patient.PatientFile.TreatmentPlanId);
                Employee employee = _employeeRepo.GetWhereId(model.EmployeeId);
                var appointment = model.ToDomain();
                if (model.Id == -1) {
                    var returnedInfo = AppointmentHelper.Plan(employee, appointment.DateRange, patient.PatientFile, patient.Appointments, treatmentPlan.SessionCount, model.SessionDuration);
                    if (returnedInfo.Item1) {
                        appointment.Id = 0;
                        _toastNotification.AddSuccessToastMessage("Afspraak gemaakt");
                        _appointmentRepo.Add(appointment);
                        return RedirectToAction("Index");
                    }
                } else {
                    if (DateTime.Now <= (appointment.DateRange.Start - new TimeSpan(24, 00, 00))) {
                        var appointment2 = _appointmentRepo.GetWhereId(appointment.Id);
                        if ((!appointment2.Employee.Equals(appointment.Employee)) && (appointment2.DateRange.Start.Equals(appointment.DateRange.Start) && appointment2.DateRange.Start.Equals(appointment.DateRange.Start))) {
                            appointment2.EmployeeId = appointment.EmployeeId;
                            appointment2.DateRange = appointment.DateRange;
                            appointment2.PatientId = appointment.PatientId;
                            _appointmentRepo.Update(appointment2);
                            _toastNotification.AddSuccessToastMessage("Afspraak gewijzigd");
                            return RedirectToAction("Index");
                        } else {
                            var returnedInfo = AppointmentHelper.Plan(employee, appointment.DateRange, patient.PatientFile, patient.Appointments, treatmentPlan.SessionCount, model.SessionDuration);
                            if (returnedInfo.Item1) {
                                appointment2.EmployeeId = appointment.EmployeeId;
                                appointment2.DateRange = appointment.DateRange;
                                appointment2.PatientId = appointment.PatientId;
                                _appointmentRepo.Update(appointment2);
                                _toastNotification.AddSuccessToastMessage("Afspraak gewijzigd");
                                return RedirectToAction("Index");
                            } else {
                                ModelState.AddModelError(returnedInfo.Item2, returnedInfo.Item3);
                            }
                        }
                    } else {
                        ModelState.AddModelError("Date", "Kan afspraak maximaal 24 uur vantevoren wijzigen");
                    }
                }

            }
            var employees = await _employeeRepo.GetAllAsync();
            var claims = await _userMgr.GetClaimsAsync(CurrentUser);
            ViewBag.Employees = PreFillOptions(claims, employees);
            return View(model);
        }

        private IEnumerable<Employee> PreFillOptions(IList<Claim> claims, IEnumerable<Employee> employees) {
            if (claims.Any(claim => claim.Value == "StudentEmployee")) {
                return employees.Where(e => e.IsTherapist).ToList();
            } else {
                return employees;
            }
        }
    }
}
