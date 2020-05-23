using FSE.API.Messages;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xunit;
namespace FSE.API.Test.Messages
{
    public class UserAddMsgTest
    {
        [Fact]
        public void GetterSetterTest()
        {
            var userAddMsgTest = new UserAddMsg
            {
                EmployeeId = "E001",
                FirstName = "F1",
                LastName = "L1"
            };

            Assert.Equal("E001", userAddMsgTest.EmployeeId);
            Assert.Equal("F1", userAddMsgTest.FirstName);
            Assert.Equal("L1", userAddMsgTest.LastName);
        }
    }
}
