using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NGK_handin3.Controllers;
using NGK_handin3.Model;
using Xunit;

namespace Test_Controller
{
    public class unit_test_login
    {
        private LoginController _uut;

        public unit_test_login()
        {
            _uut = new LoginController();
        }

        [Fact]
        public void test_login_success()
        {
            //Arrange
            User test = new User()
            {
                password = "Test",
                username = "Test"
            };
            //Act
            var result = _uut.login(test);
            //Assert
            Xunit.Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void test_login_succes_content()
        {
            //Arrange
            User test = new User()
            {
                password = "Test",
                username = "Test"
            };
            //Act
            var result = _uut.login(test);
            var okResult = result as ObjectResult;
            //Assert

            Xunit.Assert.IsType<string>(okResult.Value);
        }

        [Fact]
        public void test_login_fail()
        {
            //Arrange
            User test = new User()
            {
                password = "123",
                username = "Test"
            };
            //Act
            var result = _uut.login(test);
            //Assert
            Xunit.Assert.IsType<BadRequestResult>(result);
        }
    }
}
