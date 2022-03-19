using Domain;
using System;
using System.Collections.Generic;
using Portal.Utility;

namespace Portal.Models {
    public static class ModelHelperMethods {

        //AddPatientViewModel - check before reimplementing
        public static Patient ToDomain(this AddPatientViewModel patient) {
            return new Patient {
                Name = patient.Name,
                Dob = patient.Dob,
                RegistrationNumber = patient.RegistrationNumber,
                Gender = patient.Gender,
                EmailAdress = patient.EmailAdress,
                PhoneNumber = patient.PhoneNumber,
                Image = patient.Image.ToBase64String(),
                PatientFile = new PatientFile {
                    DiagnosisId = patient.DiagnosisId,
                    DiagnosisDescription = patient.DiagnosisDescription,
                    Condition = patient.Condition,
                    //Appointments = new List<Appointment>,
                    KindOfPatient = patient.KindOfPatient,
                    IntakeById = patient.IntakeById,
                    IntakeUnderSupervisionOfId = patient.IntakeUnderSupervisionOfId == -1 ? null : patient.IntakeUnderSupervisionOfId,
                    DateOfAdmission = patient.DateOfAdmission,
                    MainTherapistId = patient.MainTherapistId,
                    TreatmentPlan = new TreatmentPlan {
                        ProceedingCode = patient.ProceedingCode,
                        SessionCount = patient.SessionCount,
                        SessionDuration = new TimeSpan(0, 0, patient.SessionDuration, 0)
                    }
                }
            };
        }
        public static EditAppointmentViewModel ToViewModel(this Appointment appointment, TimeSpan sessionDuration) {
            return new() {
                Date = appointment.DateRange.Start.Date,
                StartTime = appointment.DateRange.Start.TimeOfDay,
                SessionDuration = sessionDuration,
                EmployeeId = appointment.EmployeeId,
                PatientId = appointment.PatientId,
                Id = appointment.Id
            };
        }
        public static Appointment ToDomain(this EditAppointmentViewModel model) {
            return new() {
                DateRange = new DateRange(model.Date.Date + model.StartTime, model.Date.Date + (model.SessionDuration + model.StartTime)),
                EmployeeId = model.EmployeeId,
                PatientId = model.PatientId,
                Id = model.Id == null ? -1 : (int)model.Id
            };
        }
        public static List<PatientInfoViewModel> ToPatientInfoViewModel(this IEnumerable<Patient> patients) {
            List<PatientInfoViewModel> patientsViewModel = new List<PatientInfoViewModel>();
            foreach (Patient patient in patients) {
                patientsViewModel.Add(patient.ToPatientInfoViewModel());
            }
            return patientsViewModel;
        }
        public static PatientInfoViewModel ToPatientInfoViewModel(this Patient patient) {
            return new PatientInfoViewModel {
                PatientId = patient.PatientId,
                Name = patient.Name,
                Dob = patient.Dob.ToString("dd-MM-yyyy"),
                Gender = patient.Gender,
                EmailAdress = patient.EmailAdress,
                PhoneNumber = patient.PhoneNumber
            };
        }
    }
}
