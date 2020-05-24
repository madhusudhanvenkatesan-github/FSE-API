using FSE.API.DomainModel;
using Xunit;
namespace FSE.API.Test.DomainModel
{
    public class PMUserTest
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
