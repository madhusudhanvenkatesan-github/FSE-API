using FSE.API.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace FSE.API.Test.DomainModel
{
    public class PMOUserTest
    {
        [Fact]
        public void GetterSetter()
        {
            var pmUser = new PMUser
            {
                EmployeeId = "E001",
                FirstName = "First",
                LastName = "Last",
                Id = "PMUser/1"
            };
            Assert.Equal("E001", pmUser.EmployeeId);
            Assert.Equal("First", pmUser.FirstName);
            Assert.Equal("Last", pmUser.LastName);
            Assert.Equal("PMUser/1", pmUser.Id);
        }
    }
}
