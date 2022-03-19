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

namespace Portal.Tests {
    public class AvailabilityTest {
        [Fact]
        public void Edit_Appointment_Should_Return_Error_When_Planning_Outside_Availability() {
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
            var employeeRepo = new Mock<IEmployeeRepo>();
            var treatmentPlanRepo = new Mock<ITreatmentPlanRepo>();
            var vektisRepo = new Mock<IVektisRepo>();
            var toastNotify = new Mock<IToastNotification>();

            var mockMapper = new MapperConfiguration(cfg => {
                cfg.AddProfile(new MappingProfiles());
            });
            var mapper = mockMapper.CreateMapper();

            var sut = new PatientController(httpContextAccessor.Object, userMgr.Object, patientRepo.Object,
                appointmentRepo.Object, employeeRepo.Object, treatmentPlanRepo.Object, vektisRepo.Object, toastNotify.Object, mapper);
            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var dateTime = DateTime.Today + new TimeSpan(10, 00, 00);

            var appointment = new EditAppointmentViewModel() {
                Date = dateTime.AddDays(1),
                StartTime = new TimeSpan(19, 00, 00),
                SessionDuration = new TimeSpan(1, 00, 00),
            };
            var patient = new Patient {
                PatientId = 1,
                Name = "Jan",
                Dob = DateTime.Today.AddYears(-20),
                Gender = Gender.Male,
                EmailAdress = "janjanssen@gmail.com",
                PhoneNumber = "0612345678",
                RegistrationNumber = 12345678,
                PatientFile = new() {
                    MainTherapistId = 1
                },
                Image = "image",
                Appointments = new[] { new Appointment { DateRange = new(dateTime.AddDays(1), dateTime.AddDays(1).AddHours(1)) } }
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


            treatmentPlanRepo.Setup(treatmentPlanRepo => treatmentPlanRepo.GetWhereId(It.IsAny<int>())).Returns(
                new TreatmentPlan {
                    Id = 1,
                    SessionDuration = new TimeSpan(1, 00, 00),
                    SessionCount = 2,
                    ProceedingCode = "F1000"
                });

            employeeRepo.Setup(employeeRepo => employeeRepo.GetWhereId(It.IsAny<int>())).Returns(employee);

            // Act
            var result = sut.EditAppointment(appointment) as ViewResult;

            // Assert
            Assert.False(result.ViewData.ModelState.IsValid);
            Assert.True(result.ViewData.ModelState.ContainsKey("StartTime"));
            Assert.Equal("De behandelaar is op deze tijd of dag niet beschikbaar", result.ViewData.ModelState["StartTime"].Errors.First().ErrorMessage);
            Assert.Null(result.ViewName);
        }
        [Fact]
        public void Edit_Appointment_Should_Return_Error_When_Planning_Simultaniously_With_Another_Appointment() {
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
            var employeeRepo = new Mock<IEmployeeRepo>();
            var treatmentPlanRepo = new Mock<ITreatmentPlanRepo>();
            var toastNotify = new Mock<IToastNotification>();
            var vektisRepo = new Mock<IVektisRepo>();

            var mockMapper = new MapperConfiguration(cfg => {
                cfg.AddProfile(new MappingProfiles());
            });
            var mapper = mockMapper.CreateMapper();

            var sut = new PatientController(httpContextAccessor.Object, userMgr.Object, patientRepo.Object, appointmentRepo.Object,
                employeeRepo.Object, treatmentPlanRepo.Object, vektisRepo.Object, toastNotify.Object, mapper);
            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var dateTime = DateTime.Today + new TimeSpan(11, 00, 00);

            var appointment = new EditAppointmentViewModel() {
                Date = dateTime.AddDays(1),
                StartTime = new TimeSpan(11, 00, 00),
                SessionDuration = new TimeSpan(1, 00, 00),
            };
            var patient = new Patient {
                PatientId = 1,
                Name = "Jan",
                Dob = DateTime.Today.AddYears(-20),
                Gender = Gender.Male,
                EmailAdress = "janjanssen@gmail.com",
                PhoneNumber = "0612345678",
                RegistrationNumber = 12345678,
                PatientFile = new() {
                    MainTherapistId = 1
                },
                Image = "image",
                Appointments = new[] { new Appointment { DateRange = new(dateTime.AddDays(1), dateTime.AddDays(1).AddHours(1)) } }
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


            treatmentPlanRepo.Setup(treatmentPlanRepo => treatmentPlanRepo.GetWhereId(It.IsAny<int>())).Returns(
                new TreatmentPlan {
                    Id = 1,
                    SessionDuration = new TimeSpan(1, 00, 00),
                    SessionCount = 2,
                    ProceedingCode = "F1000"
                });

            employeeRepo.Setup(employeeRepo => employeeRepo.GetWhereId(It.IsAny<int>())).Returns(employee);

            // Act
            var result = sut.EditAppointment(appointment) as ViewResult;

            // Assert
            Assert.False(result.ViewData.ModelState.IsValid);
            Assert.True(result.ViewData.ModelState.ContainsKey("StartTime"));
            Assert.Equal("U of uw behandelaar heeft op deze tijd al een afspraak", result.ViewData.ModelState["StartTime"].Errors.First().ErrorMessage);
            Assert.Null(result.ViewName);
        }
    }
}
