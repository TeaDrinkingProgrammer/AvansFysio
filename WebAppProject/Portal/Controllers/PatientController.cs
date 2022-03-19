using AutoMapper;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Portal.Models;
using Portal.Utility;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Portal.Controllers {
    public class PatientController : Controller {
        private IdentityUser CurrentUser;
        private IPatientRepo _patientRepo;
        private IMapper _mapper;
        private IAppointmentRepo _appointmentRepo;
        private IEmployeeRepo _employeeRepo;
        private ITreatmentPlanRepo _treatmentPlanRepo;
        private readonly IToastNotification _toastNotification;
        private IVektisRepo _vektisRepo;
        public PatientController(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userMgr, 
            IPatientRepo patientRepo, IAppointmentRepo appointmentRepo,
            IEmployeeRepo employeeRepo, ITreatmentPlanRepo treatmentPlanRepo,
            IVektisRepo vektisRepo,
            IToastNotification toastNotification, IMapper mapper) {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            CurrentUser = userMgr.FindByIdAsync(userId).Result;
            _patientRepo = patientRepo;
            _mapper = mapper;
            _appointmentRepo = appointmentRepo;
            _employeeRepo = employeeRepo;
            _treatmentPlanRepo = treatmentPlanRepo;
            _toastNotification = toastNotification;
            _vektisRepo = vektisRepo;
        }
        [Authorize(Policy = "RequirePatient")]
        public IActionResult Index() {
            var patient = _patientRepo.GetWhereEmail(CurrentUser.Email);
            var model = _mapper.Map<PatientDetailsViewModel>(patient);
            model.Remarks = model.Remarks.Where(x => x.VisibleForPatient).ToList();
            ViewBag.cultureInfo = new CultureInfo("nl-NL");
            return View(model);
        }
        public IActionResult InfoDetails() {
            var patient = _patientRepo.GetWhereEmail(CurrentUser.Email);
            var model = _mapper.Map<InfoDetailsViewModel>(patient);
            model.Image = "data:image;base64," + patient.Image;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> PatientDetailsAsync() {
            var patient = _patientRepo.GetWhereEmail(CurrentUser.Email);
            var model = _mapper.Map<PatientDetailsViewModel>(patient);
            model.Image = "data:image;base64," + patient.Image;
            model.Diagnosis = await _vektisRepo.GetDiagnosisByIdAsync(patient.PatientFile.DiagnosisId);
            ViewBag.cultureInfo = new CultureInfo("nl-NL");
            return View(model);
        } 

        [HttpGet]
        public IActionResult EditAppointment(int? id) {
            if (id == -1 || id == null) {
                var patient = _patientRepo.GetWhereEmail(CurrentUser.Email);
                var treatmentPlan = _treatmentPlanRepo.GetWhereId(patient.PatientFile.TreatmentPlanId);
                var model = new EditAppointmentViewModel {
                    SessionDuration = treatmentPlan.SessionDuration,
                    Date = DateTime.Now,
                    StartTime = new TimeSpan(14, 00, 00)
                };
                return View(model);
            } else {
                var appointment = _appointmentRepo.GetWhereId((int)id);
                return View(appointment.ToViewModel(appointment.DateRange.End - appointment.DateRange.Start));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAppointment(EditAppointmentViewModel model) {
            var patient = _patientRepo.GetWhereEmail(CurrentUser.Email);
            if (ModelState.IsValid) {
                model.EmployeeId = patient.PatientFile.MainTherapistId;
                model.PatientId = patient.PatientId;
                var treatmentPlan = _treatmentPlanRepo.GetWhereId(patient.PatientFile.TreatmentPlanId);
                var employee = _employeeRepo.GetWhereId(patient.PatientFile.MainTherapistId);
                var appointment = model.ToDomain();
                var returnedInfo = AppointmentHelper.Plan(employee, appointment.DateRange, patient.PatientFile, patient.Appointments, treatmentPlan.SessionCount, model.SessionDuration);
                if (returnedInfo.Item1) {
                    if (model.Id == -1) {
                        appointment.Id = 0;
                        _appointmentRepo.Add(appointment);
                        return RedirectToAction("Index", "Patient");
                    } else {
                        if (DateTime.Now <= (appointment.DateRange.Start - new TimeSpan(24, 00, 00))) {
                            var appointment2 = _appointmentRepo.GetWhereId(appointment.Id);
                            appointment2.EmployeeId = appointment.EmployeeId;
                            appointment2.DateRange = appointment.DateRange;
                            appointment2.PatientId = appointment.PatientId;
                            _appointmentRepo.Update(appointment2);
                            _toastNotification.AddSuccessToastMessage("Afspraak gewijzigd");
                            return RedirectToAction("Index", "Patient");
                        } else {
                            ModelState.AddModelError("Date", "Kan afspraak maximaal 24 uur vantevoren wijzigen");
                        }
                    }

                } else {
                    ModelState.AddModelError(returnedInfo.Item2, returnedInfo.Item3);
                }
            }
            return View(model);
        }
    }
}
