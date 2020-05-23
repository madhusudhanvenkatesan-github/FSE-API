using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FSE.API.DomainService;

using Microsoft.Extensions.Logging;
using FSE.API.Repository;
using FSE.API.DomainModel;
using AutoMapper;
using FSE.API.Messages;

namespace FSE.API.Test.DomainService
{
    public class ProjectTaskServiceTest
    {
        private ILogger<ProjTaskService>createProjServiceLogger()
        {
            var loggerFactory = new LoggerFactory();
            return loggerFactory.CreateLogger<ProjTaskService>();
        }
        [Fact]
        public async Task AddProject()
        {
            var projectMod = new ProjectAdd
            {
                EndDate = DateTime.Today.AddDays(2),
                StartDate = DateTime.Today,
                PMUsrId = "Usr/1",
                ProjectTitle = "Project A1",
                Priority =1

            };
            var project = new Project
            {
                End = projectMod.EndDate,
                MaxTaskCount = 0,
                PMId = projectMod.PMUsrId,
                Priority = projectMod.Priority,
                Start = projectMod.StartDate,
                Status = 0,
                Title = projectMod.ProjectTitle
            };
            var mockProjTaskRepo = new Mock<IProjectTaskRepo>();
            mockProjTaskRepo.Setup(repo => repo.AddProject(It.IsAny<Project>()))
                            .Returns(Task.FromResult(new Tuple<bool, string>(true, "P/1")));
            var mockLogger = createProjServiceLogger();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<Project>(It.IsAny<ProjectAdd>())).Returns(project);
            var projectTaskService = new ProjTaskService(mockProjTaskRepo.Object, mockLogger, mockMapper.Object);
            var result = await projectTaskService.AddProject(projectMod);
            Assert.True( result.Item1);
            Assert.Equal("P/1", result.Item2);
        }
        [Fact]
        public async Task ModifyProject()
        {
            var projectMod = new ProjectMod
            {
                EndDate = DateTime.Today.AddDays(2),
                StartDate = DateTime.Today,
                PMUsrId = "Usr/1",
                ProjectTitle = "Project A1",
                Priority = 1,
                ProjId = "P/1"

            };
            var project = new Project
            {
                End = projectMod.EndDate,
                MaxTaskCount = 0,
                PMId = projectMod.PMUsrId,
                Priority = projectMod.Priority,
                Start = projectMod.StartDate,
                Status = 0,
                Id = projectMod.ProjId

            };
            var mockProjTaskRepo = new Mock<IProjectTaskRepo>();
            mockProjTaskRepo.Setup(repo => repo.EditProject(It.IsAny<Project>()))
                            .Returns(Task.FromResult(new Tuple<bool, string>(true, "P/1")));
            var mockLogger = createProjServiceLogger();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<Project>(It.IsAny<ProjectMod>())).Returns(project);
            var projectTaskService = new ProjTaskService(mockProjTaskRepo.Object, mockLogger, mockMapper.Object);
            var result = await projectTaskService.EditProject(projectMod);
            Assert.True(result.Item1);
            Assert.Equal("P/1", result.Item2);
        }
        [Fact]
        public async Task GetAllActiveProjectTest()
        {
            var projectlsting = new ProjectListing
            {
                EndDate = DateTime.Today.AddDays(2),
                StartDate = DateTime.Today,
                PMUsrId = "Usr/1",
                ProjectTitle = "Project A1",
                Priority = 1,
                ProjId = "P/1",
                CompletedTaskCount=0,
                PMUsrName="John",
                TotalTaskCount=0

            };
            var projectlstings = new List<ProjectListing> { projectlsting };
            var projectList = new List<ProjectUserVO>
            {
                new ProjectUserVO
                {
                    Projects =new Project
                    {
                        End = projectlsting.EndDate,
                        MaxTaskCount = 0,
                        PMId = projectlsting.PMUsrId,
                        Priority = projectlsting.Priority,
                        Start = projectlsting.StartDate,
                        Status = 0,
                        Id = projectlsting.ProjId
                    },
                    UserName=projectlsting.PMUsrName

                }
            };
            var mockProjTaskRepo = new Mock<IProjectTaskRepo>();
            mockProjTaskRepo.Setup(repo => repo.GetAllActiveProject())
                           .Returns(Task.FromResult(projectList));
            var mockLogger = createProjServiceLogger();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<List<ProjectListing>>(It.IsAny<List<ProjectUserVO>>()))
                      .Returns(projectlstings);
            var projectTaskService = new ProjTaskService(mockProjTaskRepo.Object, mockLogger, mockMapper.Object);
            var result = await projectTaskService.GetAllActiveProject();
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task GetProjectByNameTest()
        {
            var projectlsting = new ProjectListing
            {
                EndDate = DateTime.Today.AddDays(2),
                StartDate = DateTime.Today,
                PMUsrId = "Usr/1",
                ProjectTitle = "Project A1",
                Priority = 1,
                ProjId = "P/1",
                CompletedTaskCount = 0,
                PMUsrName = "John",
                TotalTaskCount = 0

            };
            var projectlstings = new List<ProjectListing> { projectlsting };
            var projectList = new List<ProjectUserVO>
            {
                new ProjectUserVO
                {
                    Projects =new Project
                    {
                        End = projectlsting.EndDate,
                        MaxTaskCount = 0,
                        PMId = projectlsting.PMUsrId,
                        Priority = projectlsting.Priority,
                        Start = projectlsting.StartDate,
                        Status = 0,
                        Id = projectlsting.ProjId
                    },
                    UserName=projectlsting.PMUsrName

                }
            };
            var mockProjTaskRepo = new Mock<IProjectTaskRepo>();
            mockProjTaskRepo.Setup(repo => repo.GetProjectByName(It.IsAny<string>()))
                            .Returns(Task.FromResult(projectList));
            var mockLogger = createProjServiceLogger();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<List<ProjectListing>>(It.IsAny<List<ProjectUserVO>>()))
                      .Returns(projectlstings);
            var projectTaskService = new ProjTaskService(mockProjTaskRepo.Object, mockLogger, mockMapper.Object);
            var result = await projectTaskService.GetProjectByName("P/1");
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task SuspendProjectTest()
        {
            var mockProjTaskRepo = new Mock<IProjectTaskRepo>();
            mockProjTaskRepo.Setup(repo => repo.SuspendProject(It.IsAny<string>()))
                            .Returns(Task.FromResult(true));
            var mockLogger = createProjServiceLogger();
            var mockMapper = new Mock<IMapper>();
            var projectTaskService = new ProjTaskService(mockProjTaskRepo.Object, mockLogger, mockMapper.Object);
            var result = await projectTaskService.SuspendProject("P/1");
            Assert.True(result);
        }
        [Fact]
        public async Task GetAllActiveTaskTest()
        {
            var taskUservo1 = new TaskUserVO
            {
                ProjectId = "P/1",
                Tasks = new ProjTask
                {
                    ParentTaskId = "",
                    EndDate = DateTime.Today.AddDays(2),
                    Priority = 1,
                    Start = DateTime.Today,
                    Name = "ParentTask",
                    TaskOwnerId = "Usr/2",
                    Id = "P/1-1",
                    Status = 0
                },
                UserName = "John"
            };
            var taskUservo2 = new TaskUserVO
            {
                ProjectId = "P/1",
                Tasks = new ProjTask
                {
                    ParentTaskId = "P/1-1",
                    EndDate = DateTime.Today.AddDays(2),
                    Priority = 1,
                    Start = DateTime.Today,
                    Name = "Task1",
                    TaskOwnerId = "Usr/3",
                    Id = "P/1-2",
                    Status = 0
                },
                UserName = "Rob"
            };
            var takUserVos = new List<TaskUserVO> { taskUservo1, taskUservo2 };
            var taskListing1 = new TaskListing
            {
                ParentTaskId = "",
                ParentDescription = "No Parent Task",
                EndDate = DateTime.Today.AddDays(2),
                Priority = 1,
                ProjectId = "P/1",
                StartDate = DateTime.Today,
                TaskDescription = "ParentTask",
                TaskOwnerId = "Usr/2",
                TaskId = "P/1-1",
                Status = 0,
                TaskOwnerName = "John"
            };
            var taskListing2 = new TaskListing
            {
                ParentTaskId = "P/1-1",
                ParentDescription = "ParentTask",
                EndDate = DateTime.Today.AddDays(2),
                Priority = 1,
                ProjectId = "P/1",
                StartDate = DateTime.Today,
                TaskDescription = "Task1",
                TaskOwnerId = "Usr/3",
                TaskId = "P/1-2",
                Status = 0,
                TaskOwnerName = "Rob"
            };
            var tasklistings = new List<TaskListing> { taskListing1, taskListing2 };
            var mockProjTaskRepo = new Mock<IProjectTaskRepo>();
            mockProjTaskRepo.Setup(repo => repo.GetAllActiveTask(It.IsAny<string>()))
                            .Returns(Task.FromResult(takUserVos));
            var mockLogger = createProjServiceLogger();
            var mockMapper = new Mock<IMapper>();
            var projectTaskService = new ProjTaskService(mockProjTaskRepo.Object, mockLogger, mockMapper.Object);
            var result = await projectTaskService.GetAllActiveTask("P/1");
            Assert.NotNull(result);

        }
        [Fact]
        public async Task AddTaskTest()
        {
            var projTasks = new ProjTask
            {
                ParentTaskId = "",
                EndDate = DateTime.Today.AddDays(2),
                Priority = 1,
                Start = DateTime.Today,
                Name = "ParentTask",
                TaskOwnerId = "Usr/2",
                Status = 0
            };
            var taskAdd = new TaskAdd
            {
                ParentTaskId = "",
                EndDate = DateTime.Today.AddDays(2),
                Priority = 1,
                StartDate = DateTime.Today,
                TaskDescription = "ParentTask",
                TaskOwnerId = "Usr/2",
               ProjectId = "P/1"
            };
            var mockProjTaskRepo = new Mock<IProjectTaskRepo>();
            mockProjTaskRepo.Setup(repo => repo.AddTask(It.IsAny<string>(), It.IsAny<ProjTask>()))
                            .Returns(Task.FromResult(new Tuple<bool, string>(true, "P/1-1")));
            
            var mockLogger = createProjServiceLogger();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(map => map.Map<ProjTask>(It.IsAny<TaskAdd>())).Returns(projTasks);
            var projectTaskService = new ProjTaskService(mockProjTaskRepo.Object, mockLogger, mockMapper.Object);
            var result = await projectTaskService.AddTask(taskAdd);
            Assert.True(result.Item1);
            Assert.Equal("P/1-1", result.Item2);
        }
        [Fact]
        public async Task EditTaskTest()
        {

            var projTasks = new ProjTask
            {
                ParentTaskId = "",
                EndDate = DateTime.Today.AddDays(2),
                Priority = 1,
                Start = DateTime.Today,
                Name = "ParentTask",
                TaskOwnerId = "Usr/2",
                Status = 0
            };
            var taskMod = new TaskMod
            {
                ParentTaskId = "",
                EndDate = DateTime.Today.AddDays(2),
                Priority = 1,
                StartDate = DateTime.Today,
                TaskDescription = "ParentTask",
                TaskOwnerId = "Usr/2",
                ProjectId = "P/1",
                TaskId = "P/1-1"
            };
            var mockProjTaskRepo = new Mock<IProjectTaskRepo>();
            mockProjTaskRepo.Setup(repo => repo.EditTask(It.IsAny<string>(), It.IsAny<ProjTask>()))
                            .Returns(Task.FromResult(new Tuple<bool, string>(true, "P/1-1")));
            var mockLogger = createProjServiceLogger();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(map => map.Map<ProjTask>(It.IsAny<TaskMod>())).Returns(projTasks);
            var projectTaskService = new ProjTaskService(mockProjTaskRepo.Object, mockLogger, mockMapper.Object);
            var result = await projectTaskService.EditTask(taskMod);
            Assert.True(result.Item1);
            Assert.Equal("P/1-1", result.Item2);
        }
        [Fact]
        public async Task EndTaskTest()
        {
            var taskUservo1 = new TaskUserVO
            {
                ProjectId = "P/1",
                Tasks = new ProjTask
                {
                    ParentTaskId = "",
                    EndDate = DateTime.Today.AddDays(2),
                    Priority = 1,
                    Start = DateTime.Today,
                    Name = "ParentTask",
                    TaskOwnerId = "Usr/2",
                    Id = "P/1-1",
                    Status = 0
                },
                UserName = "John"
            };
            var taskUservo2 = new TaskUserVO
            {
                ProjectId = "P/1",
                Tasks = new ProjTask
                {
                    ParentTaskId = "P/1-1",
                    EndDate = DateTime.Today.AddDays(2),
                    Priority = 1,
                    Start = DateTime.Today,
                    Name = "Task1",
                    TaskOwnerId = "Usr/3",
                    Id = "P/1-2",
                    Status = 0
                },
                UserName = "Rob"
            };
            var takUserVos = new List<TaskUserVO> { taskUservo1, taskUservo2 };
            var taskListing1 = new TaskListing
            {
                ParentTaskId = "",
                ParentDescription = "No Parent Task",
                EndDate = DateTime.Today.AddDays(2),
                Priority = 1,
                ProjectId = "P/1",
                StartDate = DateTime.Today,
                TaskDescription = "ParentTask",
                TaskOwnerId = "Usr/2",
                TaskId = "P/1-1",
                Status = 0,
                TaskOwnerName = "John"
            };
            var taskListing2 = new TaskListing
            {
                ParentTaskId = "P/1-1",
                ParentDescription = "ParentTask",
                EndDate = DateTime.Today.AddDays(2),
                Priority = 1,
                ProjectId = "P/1",
                StartDate = DateTime.Today,
                TaskDescription = "Task1",
                TaskOwnerId = "Usr/3",
                TaskId = "P/1-2",
                Status = 0,
                TaskOwnerName = "Rob"
            };
            var tasklistings = new List<TaskListing> { taskListing1, taskListing2 };
            var mockProjTaskRepo = new Mock<IProjectTaskRepo>();
            mockProjTaskRepo.Setup(repo => repo.GetAllActiveTask(It.IsAny<string>()))
                           .Returns(Task.FromResult(takUserVos));
            mockProjTaskRepo.Setup(repo => repo.EndTask(It.IsAny<string>(), It.IsAny<string>()))
                            .Returns(Task.FromResult(new Tuple<bool, string>(true, "P/1-1")));

            var mockLogger = createProjServiceLogger();
            var mockMapper = new Mock<IMapper>();
            var projectTaskService = new ProjTaskService(mockProjTaskRepo.Object, mockLogger, mockMapper.Object);
            var result = await projectTaskService.EndTask("P/1","P/1-2");
            Assert.True(result.Item1);

            await Assert.ThrowsAnyAsync<ApplicationException>(async () => await projectTaskService.EndTask("P/1", "P/1-1"));
        }
    }
}
