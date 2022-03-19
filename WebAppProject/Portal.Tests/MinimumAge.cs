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
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Portal.Tests {
    public class MinimumAgeTest {
        [Theory]
        [InlineData(11, false)]
        [InlineData(15, false)]
        public void Patient_Add_View_Model_Should_Return_Error_When_Patient_Is_Less_Than_16_Years_Old(int age, bool expectedResult) {
            // Arrange
            var validationResults = new List<ValidationResult>();

            var imageMock = new Mock<IFormFile>();
            var content = "Hello World";
            var fileName = "veryinterestingfile.txt";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            imageMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            imageMock.Setup(_ => _.FileName).Returns(fileName);
            imageMock.Setup(_ => _.Length).Returns(ms.Length);

            var sut = new AddPatientViewModel() {
                Name = "Jan",
                Dob = DateTime.Today.AddYears(age),
                Gender = Gender.Male,
                EmailAdress = "janjanssen@gmail.com",
                PhoneNumber = "0612345678",
                RegistrationNumber = 12345678,
                MainTherapistId = 1,
                Image = imageMock.Object,
                Condition = "condition",
                ProceedingCode = "F1000"
            };

            var validationContext = new ValidationContext(sut, null, null);

            //Act
            var result = Validator.TryValidateObject(sut, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.Contains(validationResults, validationResult => validationResult.ErrorMessage.Contains("U moet minimaal 16 jaar oud zijn"));
        }
    }
}
