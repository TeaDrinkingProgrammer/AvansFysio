using System;
using Xunit;
using Moq;
using Infrastructure;
using Portal.Controllers;
using Microsoft.Extensions.Logging;
using Domain;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using DomainServices;
using AutoMapper;
using NToastNotify;
using System.Threading.Tasks;

namespace Portal.Tests {
    public class TreatmentDate {
        [Fact]
        public void Edit_Treatment_Should_Return_Error_When_Treatment_Date_Is_After_Discharge_Date() {
            //Arrange
            //https://gunnarpeipman.com/aspnet-core-test-controller-fake-user/
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, "Jan Janssen"),
                new Claim(ClaimTypes.Email,"janjanssen@gmail.com")
                }, "mock"));
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim(ClaimTypes.NameIdentifier, "1"));
            var store = new Mock<IUserStore<IdentityUser>>();
            var userMgr = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
            userMgr.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new IdentityUser {
                Email = "janjanssen@gmail.com"
            });

            var patientRepo = new Mock<IPatientRepo>();
            var appointmentRepo = new Mock<IAppointmentRepo>();
            var patientFileRepo = new Mock<IPatientFileRepo>();
            var vektisRepo = new Mock<IVektisRepo>();
            var treatmentRepo = new Mock<ITreatmentRepo>();
            var employeeRepo = new Mock<IEmployeeRepo>();
            var treatmentPlanRepo = new Mock<ITreatmentPlanRepo>();
            var toastNotify = new Mock<IToastNotification>();

            var mockMapper = new MapperConfiguration(cfg => {
                cfg.AddProfile(new MappingProfiles());
            });
            var mapper = mockMapper.CreateMapper();

            var sut = new EmployeeController(httpContextAccessor.Object, userMgr.Object, patientRepo.Object, patientFileRepo.Object, vektisRepo.Object, mapper, employeeRepo.Object, treatmentPlanRepo.Object, appointmentRepo.Object, treatmentRepo.Object, toastNotify.Object);
            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var dateTime = DateTime.Today + new TimeSpan(11, 00, 00);

            var appointment = new EditAppointmentViewModel() {
                Date = dateTime.AddDays(4),
                StartTime = new TimeSpan(10, 00, 00),
                SessionDuration = new TimeSpan(1, 00, 00),
            };
            var patientFile = new PatientFile {
                MainTherapistId = 1,
                DateOfAdmission = dateTime,
                DateOfDischarge = dateTime.AddDays(30),
                Remarks = new()
            };
            var patient = new Patient {
                PatientId = 1,
                Name = "Jan",
                Dob = DateTime.Today.AddYears(-20),
                Gender = Gender.Male,
                EmailAdress = "janjanssen@gmail.com",
                PhoneNumber = "0612345678",
                RegistrationNumber = 12345678,
                PatientFile = patientFile,
                Image = "image",
                Appointments = new[] { new Appointment { DateRange = new(dateTime.AddDays(1), dateTime.AddDays(1).AddHours(1)) }, new Appointment { DateRange = new(dateTime.AddDays(2), dateTime.AddDays(2).AddHours(1)) } }
            };
            var employee = new Employee() {
                Appointments = new[] { new Appointment { DateRange = new(dateTime.AddDays(3), dateTime.AddDays(3).AddHours(1)), Patient = patient } },
                Availibility = new Availability[] {
                    new() { Id = 1, StartHour = 0, EndHour = 0, EmployeeId = 1 },
                    new() { Id = 2, StartHour = 9, EndHour = 17, EmployeeId = 1 },
                    new() { Id = 3, StartHour = 9, EndHour = 17, EmployeeId = 1 },
                    new() { Id = 4, StartHour = 9, EndHour = 17, EmployeeId = 1 },
                    new() { Id = 5, StartHour = 9, EndHour = 17, EmployeeId = 1 },
                    new() { Id = 6, StartHour = 9, EndHour = 15, EmployeeId = 1 },
                    new() { Id = 7, StartHour = 0, EndHour = 0, EmployeeId = 1 },
                },
                EmailAdress = "therapist@gmail.com",
                Name = "therapist"
            };

            patientRepo.Setup(patientRepo => patientRepo.GetWhereEmail(It.IsAny<string>())).Returns(patient);

            var proceeding = new VektisProceeding {
                ExplanationRequired = "Ja",
                Description = "test"
            };
            vektisRepo.Setup(x => x.GetVektisProceedingByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(proceeding));

            treatmentPlanRepo.Setup(treatmentPlanRepo => treatmentPlanRepo.GetWhereId(It.IsAny<int>())).Returns(
                new TreatmentPlan {
                    Id = 1,
                    SessionDuration = new TimeSpan(1, 00, 00),
                    SessionCount = 2,
                    ProceedingCode = "F1000"
                });

            employeeRepo.Setup(employeeRepo => employeeRepo.GetWhereId(It.IsAny<int>())).Returns(employee);

            var model = new EditTreatmentViewModel() {
                Date = dateTime.AddDays(100),
                CarriedOutById = 1,
                Id = 1,
                PatientId = 1,
                ProceedingCode = 1,
                Room = LocationEnum.Behandelruimte,
                TreatmentRemarks = "some remark",
                VisibleForPatient = false
            };
            patientFileRepo.Setup(x => x.GetWherePatientId(It.IsAny<int>())).Returns(patientFile);
            patientFileRepo.Setup(x => x.Update(It.IsAny<PatientFile>()));
            treatmentPlanRepo.Setup(x => x.Update(It.IsAny<TreatmentPlan>()));
            // Act
            var result = sut.EditTreatmentAsync(model).Result as ViewResult;

            // Assert
            Assert.False(result.ViewData.ModelState.IsValid);
            Assert.True(result.ViewData.ModelState.ContainsKey("Date"));
            Assert.Equal("Datum moet tussen binnenkomst en ontslagdatum liggen", result.ViewData.ModelState["Date"].Errors.First().ErrorMessage);        }
        [Fact]
        public void Edit_Treatment_Should_Return_Error_When_Not_Including_Remark_When_It_Is_Required() {
            //Arrange
            //https://gunnarpeipman.com/aspnet-core-test-controller-fake-user/
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, "Jan Janssen"),
                new Claim(ClaimTypes.Email,"janjanssen@gmail.com")
                }, "mock"));
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim(ClaimTypes.NameIdentifier, "1"));
            var store = new Mock<IUserStore<IdentityUser>>();
            var userMgr = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
            userMgr.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new IdentityUser {
                Email = "janjanssen@gmail.com"
            });

            var patientRepo = new Mock<IPatientRepo>();
            var appointmentRepo = new Mock<IAppointmentRepo>();
            var patientFileRepo = new Mock<IPatientFileRepo>();
            var vektisRepo = new Mock<IVektisRepo>();
            var treatmentRepo = new Mock<ITreatmentRepo>();
            var employeeRepo = new Mock<IEmployeeRepo>();
            var treatmentPlanRepo = new Mock<ITreatmentPlanRepo>();
            var toastNotify = new Mock<IToastNotification>();

            var mockMapper = new MapperConfiguration(cfg => {
                cfg.AddProfile(new MappingProfiles());
            });
            var mapper = mockMapper.CreateMapper();

            var sut = new EmployeeController(httpContextAccessor.Object, userMgr.Object, patientRepo.Object, patientFileRepo.Object, vektisRepo.Object, mapper, employeeRepo.Object, treatmentPlanRepo.Object, appointmentRepo.Object, treatmentRepo.Object, toastNotify.Object);
            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var dateTime = DateTime.Today + new TimeSpan(11, 00, 00);

            var appointment = new EditAppointmentViewModel() {
                Date = dateTime.AddDays(4),
                StartTime = new TimeSpan(10, 00, 00),
                SessionDuration = new TimeSpan(1, 00, 00),
            };
            var patientFile = new PatientFile {
                MainTherapistId = 1,
                DateOfAdmission = dateTime,
                DateOfDischarge = dateTime.AddDays(30),
                Remarks = new()
            };
            var patient = new Patient {
                PatientId = 1,
                Name = "Jan",
                Dob = DateTime.Today.AddYears(-20),
                Gender = Gender.Male,
                EmailAdress = "janjanssen@gmail.com",
                PhoneNumber = "0612345678",
                RegistrationNumber = 12345678,
                PatientFile = patientFile,
                Image = "image",
                Appointments = new[] { new Appointment { DateRange = new(dateTime.AddDays(1), dateTime.AddDays(1).AddHours(1)) }, new Appointment { DateRange = new(dateTime.AddDays(2), dateTime.AddDays(2).AddHours(1)) } }
            };
            var employee = new Employee() {
                Appointments = new[] { new Appointment { DateRange = new(dateTime.AddDays(3), dateTime.AddDays(3).AddHours(1)), Patient = patient } },
                Availibility = new Availability[] {
                    new() { Id = 1, StartHour = 0, EndHour = 0, EmployeeId = 1 },
                    new() { Id = 2, StartHour = 9, EndHour = 17, EmployeeId = 1 },
                    new() { Id = 3, StartHour = 9, EndHour = 17, EmployeeId = 1 },
                    new() { Id = 4, StartHour = 9, EndHour = 17, EmployeeId = 1 },
                    new() { Id = 5, StartHour = 9, EndHour = 17, EmployeeId = 1 },
                    new() { Id = 6, StartHour = 9, EndHour = 15, EmployeeId = 1 },
                    new() { Id = 7, StartHour = 0, EndHour = 0, EmployeeId = 1 },
                },
                EmailAdress = "therapist@gmail.com",
                Name = "therapist"
            };

            patientRepo.Setup(patientRepo => patientRepo.GetWhereEmail(It.IsAny<string>())).Returns(patient);

            var proceeding = new VektisProceeding {
                ExplanationRequired = "Ja",
                Description = "test"
            };
            vektisRepo.Setup(x => x.GetVektisProceedingByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(proceeding));

            treatmentPlanRepo.Setup(treatmentPlanRepo => treatmentPlanRepo.GetWhereId(It.IsAny<int>())).Returns(
                new TreatmentPlan {
                    Id = 1,
                    SessionDuration = new TimeSpan(1, 00, 00),
                    SessionCount = 2,
                    ProceedingCode = "F1000"
                });

            employeeRepo.Setup(employeeRepo => employeeRepo.GetWhereId(It.IsAny<int>())).Returns(employee);

            var model = new EditTreatmentViewModel() {
                Date = dateTime.AddDays(5),
                CarriedOutById = 1,
                Id = 1,
                PatientId = 1,
                ProceedingCode = 1,
                Room = LocationEnum.Behandelruimte,
                VisibleForPatient = false
            };
            patientFileRepo.Setup(x => x.GetWherePatientId(It.IsAny<int>())).Returns(patientFile);
            patientFileRepo.Setup(x => x.Update(It.IsAny<PatientFile>()));
            treatmentPlanRepo.Setup(x => x.Update(It.IsAny<TreatmentPlan>()));
            // Act
            var result = sut.EditTreatmentAsync(model).Result as ViewResult;

            // Assert
            Assert.False(result.ViewData.ModelState.IsValid);
            Assert.True(result.ViewData.ModelState.ContainsKey("TreatmentRemarks"));
            Assert.Equal("Bijzonderheden moeten ingevuld worden", result.ViewData.ModelState["TreatmentRemarks"].Errors.First().ErrorMessage);
        }
    }
}
