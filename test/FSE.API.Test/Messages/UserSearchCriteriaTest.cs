using FSE.API.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace FSE.API.Test.Messages
{
    public class UserSearchCriteriaTest
    {
        [Fact]
        public void GetterSetterTest()
        {
            var searchCrit = new UserSearchCriteria
            {
                EmployeeID = "E001",
                FirstName = "F1",
                LastName = "L1"
            };
            Assert.Equal("E001", searchCrit.EmployeeID);
            Assert.Equal("F1", searchCrit.FirstName);
            Assert.Equal("L1", searchCrit.LastName);
        }
    }
}
