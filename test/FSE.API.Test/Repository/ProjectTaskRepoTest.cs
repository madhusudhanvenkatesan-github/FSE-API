using Microsoft.Extensions.Logging;
using FSE.API.DomainModel;
using FSE.API.Repository;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace FSE.API.Test.Repository
{
    [Collection("RepoUnitTest")]
    public class ProjectTaskRepoTest
    {
        private readonly IDocumentStore DocStore;
        public ProjectTaskRepoTest(DocumentStoreClassFixture fixture) { 
            DocStore = fixture.Store;
            ManageUserCollection().GetAwaiter().GetResult();
        }
        private ILogger<ProjectTaskRepo> createProjectTaskLogger()
        {
            var loggerFactory = new LoggerFactory();
            return loggerFactory.CreateLogger<ProjectTaskRepo>();
        }
        private ILogger<UserRepo> createUserLogger()
        {
            var loggerFactory = new LoggerFactory();
            return loggerFactory.CreateLogger<UserRepo>();
        }
        protected async  Task ManageUserCollection()
        {
            
            var pmoUser1 = new PMUser
            {
                EmployeeId = "EP001",
                LastName = "L1",
                FirstName = "F1"
            };
            var pmoUser2 = new PMUser
            {
                EmployeeId = "EP002",
                LastName = "L2",
                FirstName = "F2"
            };
            var pmoUser3 = new PMUser
            {
                EmployeeId = "EP003",
                LastName = "L3",
                FirstName = "F3"
            };
            var pmoUser4 = new PMUser
            {
                EmployeeId = "E004",
                LastName = "L4",
                FirstName = "F4"
            };
            var pmoUser5 = new PMUser
            {
                EmployeeId = "EP005",
                LastName = "L5",
                FirstName = "F5"
            };
            using (var session = DocStore.OpenAsyncSession())
            {
                var userCollectionLock = await session.Query<UserCollectionLock>()
                                                      .FirstOrDefaultAsync();
                if ((userCollectionLock == default) || (!userCollectionLock.IsUserCollectionAdded))
                {
                    var logger = createUserLogger();
                    var userRepo = new UserRepo(session, logger);
                    var result = await userRepo.AddUser(pmoUser1);
                    result = await userRepo.AddUser(pmoUser2);
                    result = await userRepo.AddUser(pmoUser3);
                    result = await userRepo.AddUser(pmoUser4);
                    result = await userRepo.AddUser(pmoUser5);
                    userCollectionLock = new UserCollectionLock { IsUserCollectionAdded = true };
                    await session.StoreAsync(userCollectionLock);
                    await session.SaveChangesAsync();
                }

            }
        }
        [Fact]
        public async Task CUProjectTaskTest()
        {
            
            var project = new Project {
                End = DateTime.Today.AddDays(1),
                PMId="EP001",
                Priority=1,
                Start=DateTime.Today,
                Status = 0,
                Title = "Project A"
            };
            var logger = createProjectTaskLogger();
            using (var session = DocStore.OpenAsyncSession())
            {
                var usrlogger = createUserLogger();
                var userRepo = new UserRepo(session, usrlogger);
                var projTaskRepo = new ProjectTaskRepo(session, logger);
                
                var projAddResult = await projTaskRepo.AddProject(project);
                Assert.True(projAddResult.Item1);
                Assert.NotEmpty(projAddResult.Item2);
                
                var projectMod = new Project
                {
                    End = DateTime.Today.AddDays(1),
                    PMId = ((await userRepo.GetUserByEmployeeId("EP001"))?.Id),
                    Priority = 1,
                    Start = DateTime.Today,
                    Status = 0,
                    Title = "Project A1",
                    Id = projAddResult.Item2
                };
                var projEditResult = await projTaskRepo.EditProject(projectMod);
                Assert.True(projEditResult.Item1);
                Assert.NotEmpty(projEditResult.Item2);
                
                var task1 = new ProjTask {
                    EndDate = DateTime.Today.AddDays(2),
                    Name = "Task 1",
                    Priority=1,
                    Start=DateTime.Today,
                    Status=0,
                    TaskOwnerId= ((await userRepo.GetUserByEmployeeId("EP002"))?.Id)
                };
                var projTskAddResult = await projTaskRepo.AddTask(projEditResult.Item2, task1);
                Assert.True(projTskAddResult.Item1);
                Assert.NotEmpty(projTskAddResult.Item2);
               
                var taskChild1 = new ProjTask
                {
                    EndDate = DateTime.Today.AddDays(2),
                    Name = "Task 1-1",
                    Priority = 1,
                    Start = DateTime.Today,
                    Status = 0,
                    TaskOwnerId = ((await userRepo.GetUserByEmployeeId("EP003"))?.Id),
                    ParentTaskId= projTskAddResult.Item2
                };
                var projTskChldAddResult = await projTaskRepo.AddTask(projEditResult.Item2, taskChild1);
                Assert.NotEmpty(projTskChldAddResult.Item2);
              
                var taskMod = new ProjTask
                {
                    EndDate = DateTime.Today.AddDays(2),
                    Name = "Task A1",
                    Priority = 1,
                    Start = DateTime.Today,
                    Status = 0,
                    TaskOwnerId = ((await userRepo.GetUserByEmployeeId("EP002"))?.Id),
                    Id = "P/1-1"
                };
                var projTskEditResult = await projTaskRepo.EditTask("P/1", taskMod);
                Assert.True(projTskAddResult.Item1);
                Assert.NotEmpty(projTskAddResult.Item2);
                
                var projTaskGetAllTask = await projTaskRepo.GetAllActiveTask("P/1");
                Assert.NotEmpty(projTaskGetAllTask);
                
                var projGetProjByName = await projTaskRepo.GetProjectByName("A1");
                Assert.NotEmpty(projGetProjByName);

                var projTaskGetAllActiveProjects = await projTaskRepo.GetAllActiveProject();
                Assert.NotEmpty(projTaskGetAllActiveProjects);

                var getProjectCount = await projTaskRepo.GetProjectCountByPM(((await userRepo.GetUserByEmployeeId("EP001"))?.Id));
                Assert.True(getProjectCount>0);
                
                var getTaskCount = await projTaskRepo.GetTaskCountByUser((await userRepo.GetUserByEmployeeId("EP002"))?.Id);
                Assert.True(getTaskCount > 0);

                var endTaskResult = await projTaskRepo.EndTask("P/1", "P/1-2");
                Assert.True(endTaskResult.Item1);
                var suspenResult = await projTaskRepo.SuspendProject("P/1");
                Assert.True(endTaskResult.Item1);
            }

        }
    }
}
