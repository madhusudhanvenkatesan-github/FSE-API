using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.TestDriver;
using Raven.Client.Documents.Session;
using Microsoft.Extensions.Logging;
using FSE.API.Repository;
using FSE.API.DomainModel;
using FSE.API.Messages;
[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace FSE.API.Test.Repository
{
    [Collection("RepoUnitTest")]
    public class UserRepoTest
    {
        private readonly IDocumentStore DocStore;
        public UserRepoTest(DocumentStoreClassFixture fixture) => DocStore = fixture.Store;
        
        private ILogger<UserRepo> createUserLogger()
        {
            var loggerFactory = new LoggerFactory();
            return loggerFactory.CreateLogger<UserRepo>();
        }
        [Fact]
        public async Task CURDUserTest()
        {
            var pmUser = new PMUser
            {
                EmployeeId = "E001",
                LastName = "L1",
                FirstName = "F1"
            };
           
                using (var session = DocStore.OpenAsyncSession())
                {
                    var logger = createUserLogger();
                    var userRepo = new UserRepo(session, logger);
                    var result = await userRepo.AddUser(pmUser);

                    Assert.True(result.Item1);
                    pmUser.LastName = "Last";
                    result = await userRepo.EditUser(pmUser);
                    Assert.True(result.Item1);
                    var searchCrit = new UserSearchCriteria
                    {
                        EmployeeID = "E001",
                        LastName = "L1",
                        FirstName = "F1"
                    };
                   

                    var delResult = await userRepo.DeleteUser(pmUser);
                    Assert.True(delResult);

                }
            
        }
        [Fact]
        public async Task RetrieveUserTest()
        {
            var pmUser = new PMUser
            {
                EmployeeId = "E001",
                LastName = "L1",
                FirstName = "F1"
            };
            
                using (var session = DocStore.OpenAsyncSession())
                {
                    var logger = createUserLogger();
                    var userRepo = new UserRepo(session, logger);
                    var addresult = await userRepo.AddUser(pmUser);
                    var searchCrit = new UserSearchCriteria
                    {
                        EmployeeID = "E001",
                        LastName = "L1",
                        FirstName = "F1"
                    };
                    var getResultAny = await userRepo.GetAllUserMatchAnyCriteria(searchCrit);
                    Assert.True(getResultAny.Count>0);
                    var getResultAll = await userRepo.GetAllUser();
                    Assert.True(getResultAll.Count>0);
                    var getResultByEmpId = await userRepo.GetUserByEmployeeId(searchCrit.EmployeeID);
                    Assert.NotNull(getResultByEmpId);
                    Assert.Equal(searchCrit.EmployeeID, getResultByEmpId.EmployeeId);
                    
            }
            
        }
    }
}
