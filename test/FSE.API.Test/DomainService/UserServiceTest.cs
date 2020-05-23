using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FSE.API.Repository;
using FSE.API.DomainModel;
using FSE.API.Messages;
using AutoMapper;
using FSE.API.DomainService;
using Microsoft.Extensions.Logging;

namespace FSE.API.Test.DomainService
{
    public class UserServiceTest
    {
        private ILogger<UserService> createUserLogger()
        {
            var loggerFactory = new LoggerFactory();
            return loggerFactory.CreateLogger<UserService>();
        }
        [Fact]
        public async Task AddUserTest()
        {
            var result = new Tuple<bool, string>(true, "Usr/1");
            var userAdd = new UserAddMsg {
                EmployeeId = "EP001",
                FirstName = "F1",
                LastName = "L2"
            };
            var pmUser = new PMUser
            {
                EmployeeId = userAdd.EmployeeId,
                FirstName = userAdd.FirstName,
                LastName = userAdd.LastName
            };
            var mockUserRepo = new Mock<IUserRepo>();
            var mockProjectTaskRepo = new Mock<IProjectTaskRepo>();
            var logger = createUserLogger();
            mockUserRepo.Setup(usrRepo => usrRepo.AddUser(It.IsAny<PMUser>()))
                                               .Returns(Task.FromResult(result));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(map => map.Map<PMUser>(It.IsAny<UserAddMsg>())).Returns(pmUser);
            var userService = new UserService(mockUserRepo.Object, mockProjectTaskRepo.Object, logger,
                mockMapper.Object);
            var addResult = await userService.Add(userAdd);
            Assert.True(addResult.Item1);
            Assert.Equal("Usr/1", addResult.Item2);
        }
        [Fact]
        public async Task EditUserTest()
        {
            var result = new Tuple<bool, string>(true, "Usr/1");
            var userMod = new UserModMsg
            {
                EmployeeId = "EP001",
                FirstName = "F1",
                LastName = "L2",
                Id="Usr/1"

            };
            var pmUser = new PMUser
            {
                EmployeeId = userMod.EmployeeId,
                FirstName = userMod.FirstName,
                LastName = userMod.LastName
            };
            var mockUserRepo = new Mock<IUserRepo>();
            var mockProjectTaskRepo = new Mock<IProjectTaskRepo>();
            var logger = createUserLogger();
            mockUserRepo.Setup(usrRepo => usrRepo.EditUser(It.IsAny<PMUser>()))
                                              .Returns(Task.FromResult(result));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(map => map.Map<PMUser>(It.IsAny<UserModMsg>())).Returns(pmUser);
            var userService = new UserService(mockUserRepo.Object, mockProjectTaskRepo.Object, logger,
               mockMapper.Object);
            var modResult = await userService.Edit(userMod);
            Assert.True(modResult.Item1);
            Assert.Equal("Usr/1", modResult.Item2);
        }
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        public async Task Delete(int projUserCount, int taskUserCount)
        {
            var pmUser = new PMUser
            {
                EmployeeId = "EP001",
                FirstName = "F1",
                LastName = "L2",
                Id = "Usr/1"

            };
            var mockUserRepo = new Mock<IUserRepo>();
            var mockProjectTaskRepo = new Mock<IProjectTaskRepo>();
            var logger = createUserLogger();
            mockUserRepo.Setup(userRepo => userRepo.GetUserByEmployeeId(It.IsAny<string>()))
                        .Returns(Task.FromResult(pmUser));
            mockUserRepo.Setup(userRepo => userRepo.DeleteUser(pmUser)).Returns(Task.FromResult(true));
            var mockMapper = new Mock<IMapper>();
            mockProjectTaskRepo.Setup(projTskRepo => projTskRepo.GetProjectCountByPM(It.IsAny<string>()))
                               .Returns(Task.FromResult(projUserCount));
            mockProjectTaskRepo.Setup(projTskRepo => projTskRepo.GetTaskCountByUser(It.IsAny<string>()))
                               .Returns(Task.FromResult(taskUserCount));
            var userService = new UserService(mockUserRepo.Object, mockProjectTaskRepo.Object, logger,
               mockMapper.Object);

            if (projUserCount > 0)
                await Assert.ThrowsAsync<ApplicationException>(async () => await userService.Delete("EP001"));
            else if (taskUserCount > 0)
                await Assert.ThrowsAsync<ApplicationException>(async () => await userService.Delete("EP001"));
            else
            {
                var delResult = await userService.Delete("EP001");
                Assert.True(delResult);
            }
        }
        [Fact]
        public async Task GetAllUserTest()
        {
            var pmUser = new PMUser
            {
                EmployeeId = "EP001",
                FirstName = "F1",
                LastName = "L2",
                Id = "Usr/1"

            };
            var mockUserRepo = new Mock<IUserRepo>();
            var mockProjectTaskRepo = new Mock<IProjectTaskRepo>();
            var mockMapper = new Mock<IMapper>();
            var logger = createUserLogger();
            mockUserRepo.Setup(usrRepo => usrRepo.GetAllUser()).Returns(Task.FromResult(
                new List<PMUser> { pmUser }));
            var userService = new UserService(mockUserRepo.Object, mockProjectTaskRepo.Object, logger,
           mockMapper.Object);
            var result = await userService.GetAllUser();
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task GetUserByCriteriaTest()
        {
            var searchCrit = new UserSearchCriteria
            {
                EmployeeID = "EP001",
                FirstName = "F1",
                LastName = "L2"
            };
            var pmUser = new PMUser
            {
                EmployeeId = "EP001",
                FirstName = "F1",
                LastName = "L2",
                Id = "Usr/1"

            };
            var mockUserRepo = new Mock<IUserRepo>();
            var mockProjectTaskRepo = new Mock<IProjectTaskRepo>();
            var mockMapper = new Mock<IMapper>();
            var logger = createUserLogger();
            mockUserRepo.Setup(usrRepo => usrRepo.GetAllUserMatchAnyCriteria(It.IsAny<UserSearchCriteria>()))
                        .Returns(Task.FromResult(new List<PMUser> { pmUser }));
            var userService = new UserService(mockUserRepo.Object, mockProjectTaskRepo.Object, logger,
          mockMapper.Object);
            var result = await userService.GetUserByCriteria(searchCrit);
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task GetUserByEmployeeIdTest()
        {
            var pmUser = new PMUser
            {
                EmployeeId = "EP001",
                FirstName = "F1",
                LastName = "L2",
                Id = "Usr/1"

            };
            var mockUserRepo = new Mock<IUserRepo>();
            var mockProjectTaskRepo = new Mock<IProjectTaskRepo>();
            var mockMapper = new Mock<IMapper>();
            var logger = createUserLogger();
            mockUserRepo.Setup(usrRepo => usrRepo.GetUserByEmployeeId(It.IsAny<string>()))
                        .Returns(Task.FromResult(pmUser));
            var userService = new UserService(mockUserRepo.Object, mockProjectTaskRepo.Object, logger,
          mockMapper.Object);
            var result = await userService.GetUserByEmployeeId("EP001");
            Assert.NotNull(result);
        }
    }
}
