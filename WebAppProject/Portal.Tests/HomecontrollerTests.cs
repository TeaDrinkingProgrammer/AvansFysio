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

namespace Portal.Tests {
    public class HomecontrollerTests {
        [Fact]
        public void Index_Should_Return_View_When_User_Has_No_Claims() {
            //Arrange
            //https://gunnarpeipman.com/aspnet-core-test-controller-fake-user/
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var sut = new HomeController();
            sut.ControllerContext = new ControllerContext();
            sut.ControllerContext.HttpContext = new DefaultHttpContext { User = user };


            //Act
            var result = sut.Index() as ViewResult;

            // Assert
            Assert.Null(result.ViewName);
        }
    }
}
