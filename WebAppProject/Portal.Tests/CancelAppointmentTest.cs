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
    public class CancelAppointmentTest {
        [Fact]
        public void Cancel_Appointment_Should_Return_Error_When_Less_Than_24_Hours_Before_Appointment() {
            //Arrange
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
            var patientFileRepo = new Mock<IPatientFileRepo>();
            var appointmentRepo = new Mock<IAppointmentRepo>();
            var employeeRepo = new Mock<IEmployeeRepo>();
            var treatmentPlanRepo = new Mock<ITreatmentPlanRepo>();
            var toastNotify = new Mock<IToastNotification>();

            var mockMapper = new MapperConfiguration(cfg => {
                cfg.AddProfile(new MappingProfiles());
            });
            var mapper = mockMapper.CreateMapper();

            var sut = new PatientInfoController(httpContextAccessor.Object, userMgr.Object, mapper, patientFileRepo.Object, employeeRepo.Object, patientRepo.Object, appointmentRepo.Object, toastNotify.Object);
            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var dateTime = DateTime.Now.AddHours(23);

            var appointment = new Appointment() { DateRange = new(dateTime, dateTime.AddHours(1)) };

            appointmentRepo.Setup(appointmentRepo => appointmentRepo.GetWhereId(It.IsAny<int>())).Returns(appointment);
            appointmentRepo.Setup(x => x.Remove(It.IsAny<Appointment>())).Verifiable();

            // Act
            var result = sut.CancelAppointment(1) as ViewResult;

            // Assert
            appointmentRepo.Verify(x => x.Remove(It.IsAny<Appointment>()), Times.Never);
        }
    }
}
