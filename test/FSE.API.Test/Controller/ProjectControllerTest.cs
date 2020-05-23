
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using FSE.API.Controllers;
using FSE.API.DomainService;
using FSE.API.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace FSE.API.Test.Controller
{
    public class ProjectControllerTest
    {
        private ILogger<ProjectController> createLogger()
        {
            var loggerFactory = new LoggerFactory();
            return loggerFactory.CreateLogger<ProjectController>();
        }
        [Fact]
        public async Task GetAllActiveProjectTest()
        {
            var projectService = new Mock<IProjTaskService>();
            var projectListing = new ProjectListing
            {
                CompletedTaskCount = 0,
                EndDate = DateTime.Today.AddDays(2),
                PMUsrId = "Usr/1",
                PMUsrName = "John",
                Priority = 1,
                ProjectTitle = "Project1",
                ProjId = "P/1",
                StartDate = DateTime.Today,
                TotalTaskCount = 2
            };
            projectService.Setup(prj => prj.GetAllActiveProject())
                          .Returns(Task.FromResult(new List<ProjectListing> { projectListing }));
            var logger = createLogger();
            var projectController = new ProjectController(projectService.Object, logger);
            var actionResult = (await projectController.GetAllActiveProject()).Result as OkObjectResult;
            Assert.NotNull(actionResult);
            var result = actionResult.Value as List<ProjectListing>;
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task GetProjectByNameTest()
        {
            var projectService = new Mock<IProjTaskService>();
            var projectListing = new ProjectListing
            {
                CompletedTaskCount = 0,
                EndDate = DateTime.Today.AddDays(2),
                PMUsrId = "Usr/1",
                PMUsrName = "John",
                Priority = 1,
                ProjectTitle = "Project1",
                ProjId = "P/1",
                StartDate = DateTime.Today,
                TotalTaskCount = 2
            };
            projectService.Setup(prj => prj.GetProjectByName(It.IsAny<string>()))
                          .Returns(Task.FromResult(new List<ProjectListing> { projectListing }));
            var logger = createLogger();
            var projectController = new ProjectController(projectService.Object, logger);
            var actionResult = (await projectController.GetProjectByName("project1")).Result as OkObjectResult;
            Assert.NotNull(actionResult);
            var result = actionResult.Value as List<ProjectListing>;
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task AddProjectTest()
        {
            var projectService = new Mock<IProjTaskService>();
            var projAdd = new ProjectAdd
            {
                EndDate = DateTime.Today.AddDays(2),
                PMUsrId = "Usr/1",
                Priority = 1,
                ProjectTitle = "Project Title",
                StartDate = DateTime.Today
            };
            projectService.Setup(repo => repo.AddProject(It.IsAny<ProjectAdd>()))
                       .Returns(Task.FromResult(new Tuple<bool, string>(true, "P/1")));
            var logger = createLogger();
            var projectController = new ProjectController(projectService.Object, logger);
            var actionResult = (await projectController.AddProject(projAdd)).Result as CreatedResult;
            Assert.NotNull(actionResult);
            Assert.Equal(201, actionResult.StatusCode);

        }
        [Fact]
        public async Task EditProjectTest()
        {
            var projectService = new Mock<IProjTaskService>();
            var projMod = new ProjectMod
            {
                EndDate = DateTime.Today.AddDays(2),
                PMUsrId = "Usr/1",
                Priority = 1,
                ProjectTitle = "Project Title",
                StartDate = DateTime.Today,
                ProjId = "P/1"
            };
            projectService.Setup(repo => repo.EditProject(It.IsAny<ProjectMod>()))
                      .Returns(Task.FromResult(new Tuple<bool, string>(true, "P/1")));
            var logger = createLogger();
            var projectController = new ProjectController(projectService.Object, logger);
            var actionResult = (await projectController.EditProject(projMod)).Result as AcceptedResult;
            Assert.NotNull(actionResult);
            Assert.Equal(202, actionResult.StatusCode);
        }
        [Fact]
        public async Task SuspendProject()
        {
            var projectService = new Mock<IProjTaskService>();
            projectService.Setup(repo => repo.SuspendProject(It.IsAny<string>()))
                      .Returns(Task.FromResult(true));
            var logger = createLogger();
            var projectController = new ProjectController(projectService.Object, logger);
            var actionResult = (await projectController.SuspendProject("P/1")).Result as AcceptedResult;
            Assert.NotNull(actionResult);
            Assert.Equal(202, actionResult.StatusCode);
        }
        [Fact]
        public async Task GetAllActiveTaskTest()
        {
            var projectService = new Mock<IProjTaskService>();
            var takListing = new TaskListing
            {
                EndDate = DateTime.Today.AddDays(2),
                ParentDescription = "Task1",
                ParentTaskId = "P/1-1",
                Priority = 1,
                ProjectId = "P/1",
                StartDate = DateTime.Today,
                Status = 1,
                TaskDescription = "Task2",
                TaskId = "P/1-2",
                TaskOwnerId = "Usr/1",
                TaskOwnerName = "John"
            };
            var taskListings = new List<TaskListing> { takListing };
            projectService.Setup(ser => ser.GetAllActiveTask(It.IsAny<string>()))
                          .Returns(Task.FromResult(taskListings));
            var logger = createLogger();
            var projectController = new ProjectController(projectService.Object, logger);
            var actionResult = (await projectController.GetAllActiveTask("P/1")).Result as OkObjectResult;
            Assert.NotNull(actionResult);
            var result = actionResult.Value as List<TaskListing>;
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task AddTaskTest()
        {
            var taskAdd = new TaskAdd
            {
                EndDate = DateTime.Today.AddDays(2),
                ParentTaskId = "P/1-1",
                Priority = 1,
                ProjectId = "P/1",
                StartDate = DateTime.Today,
                TaskDescription = "Task1",
                TaskOwnerId = "Usr/1"
            };
            var projectService = new Mock<IProjTaskService>();
            projectService.Setup(ser => ser.AddTask(It.IsAny<TaskAdd>()))
                          .Returns(Task.FromResult(new Tuple<bool, string>(true, "P/1-1")));
            var logger = createLogger();
            var projectController = new ProjectController(projectService.Object, logger);
            var actionResult = (await projectController.AddTask(taskAdd)).Result as CreatedResult;
            Assert.NotNull(actionResult);
            Assert.Equal(201, actionResult.StatusCode);
        }
        [Fact]
        public async Task EditTaskTest()
        {
            var taskMod = new TaskMod
            {
                EndDate = DateTime.Today.AddDays(2),
                ParentTaskId = "P/1-1",
                Priority = 1,
                ProjectId = "P/1",
                StartDate = DateTime.Today,
                TaskDescription = "Task1",
                TaskId = "P/1-2",
                TaskOwnerId = "Usr/1"
            };
            var projectService = new Mock<IProjTaskService>();
            projectService.Setup(ser => ser.EditTask(It.IsAny<TaskMod>()))
                           .Returns(Task.FromResult(new Tuple<bool, string>(true, "P/1-1")));
            var logger = createLogger();
            var projectController = new ProjectController(projectService.Object, logger);
            var actionResult = (await projectController.EditTask(taskMod)).Result as AcceptedResult;
            Assert.NotNull(actionResult);
            Assert.Equal(202, actionResult.StatusCode);
        }
        [Fact]
        public async Task EndTaskTest()
        {
            var projectService = new Mock<IProjTaskService>();
            projectService.Setup(ser => ser.EndTask(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(Task.FromResult(new Tuple<bool, string>(true, "P/1-1")));
            var logger = createLogger();
            var projectController = new ProjectController(projectService.Object, logger);
            var actionResult = (await projectController.EndTask("P/1","P/1-1")).Result as AcceptedResult;
            Assert.NotNull(actionResult);
            Assert.Equal(202, actionResult.StatusCode);
        }
    }
}
