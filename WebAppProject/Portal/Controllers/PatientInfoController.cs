using AutoMapper;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NToastNotify;

namespace Portal.Controllers {
    public class PatientInfoController : Controller {
        private IdentityUser user;
        private IMapper _mapper;
        private IPatientFileRepo _patientFileRepo;
        private IEmployeeRepo _employeeRepo;
        private IPatientRepo _patientRepo;
        private UserManager<IdentityUser> _userMgr;
        private IAppointmentRepo _appointmentRepo;
        private IToastNotification _toastNotification;
        public PatientInfoController(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userMgr, IMapper mapper, IPatientFileRepo patientFileRepo, IEmployeeRepo employeeRepo, IPatientRepo patientRepo,IAppointmentRepo appointmentRepo,IToastNotification toastNotification) {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            user = userMgr.FindByIdAsync(userId).Result;
            _mapper = mapper;
            _patientFileRepo = patientFileRepo;
            _employeeRepo = employeeRepo;
            _patientRepo = patientRepo;
            _userMgr = userMgr;
            _appointmentRepo = appointmentRepo;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RemarksDetailAsync(int id) {
            var claims = await _userMgr.GetClaimsAsync(user);
            var patientFile = _patientFileRepo.GetWhereId(id);
            if (claims.Any(claim => claim.Value == "Patient")) {
                var patient = _patientRepo.GetWhereEmail(user.Email);
                if (patient.PatientFileId != id) {
                    //TODO make error page?
                    return RedirectToAction("PatientInfo", "RemarksDetail");
                }
            }
            ViewBag.Id = id;
            var remarks = patientFile.Remarks;
            var model = _mapper.Map<RemarkViewModel[]>(remarks);
            return View(model);
        }
        [Authorize(Policy = "RequireEmployee")]
        [HttpGet]
        public IActionResult RemarkEdit(int id) {
            RemarkViewModel model = new() {
                PatientFileId = id,
                VisibleForPatient = false
            };
            return View(model);
        }

        [Authorize(Policy = "RequireEmployee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemarkEdit(RemarkViewModel model) {
            if (ModelState.IsValid) {
                var remarkBy = _employeeRepo.GetWhereEmail(user.Email);
                var remark = _mapper.Map<Remark>(model);
                remark.RemarkBy = remarkBy;
                remark.Date = DateTime.Now;
                var patientFile = _patientFileRepo.GetWhereId(model.PatientFileId);
                if (patientFile.Remarks == null) {
                    patientFile.Remarks = new Collection<Remark>();
                }
                patientFile.Remarks.Add(remark);
                _patientFileRepo.Update(patientFile);
                return RedirectToAction("Index", "Employee");
            } else {
                return View(model);
            }
        }
        public IActionResult CancelAppointment(int id) {
            var appointment = _appointmentRepo.GetWhereId(id);
            if (DateTime.Now <= (appointment.DateRange.Start - new TimeSpan(24, 00, 00))) {
                _appointmentRepo.Remove(appointment);
                _toastNotification.AddSuccessToastMessage("Afspraak afgezegd");
            } else {
                _toastNotification.AddErrorToastMessage("Kan afspraak maximaal 24 uur vantevoren afzeggen");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
