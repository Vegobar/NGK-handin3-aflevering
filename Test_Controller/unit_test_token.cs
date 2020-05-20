using System;
using System.Collections.Generic;
using System.Text;
using NGK_handin3.Token;
using Xunit;

namespace Test_Controller
{
    public class unit_test_token
    {
        private TokenManager _uut;

        public unit_test_token()
        {
            _uut = new TokenManager();
        }

        [Fact]
        public void test_token()
        {
            //Arrange
            //Act
            var result = _uut.GenerateToken("Test");
            //Assert
            Xunit.Assert.IsType<string>(result);
        }
    }
}
