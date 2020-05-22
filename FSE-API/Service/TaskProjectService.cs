using AutoMapper;
using Microsoft.Extensions.Logging;
using FSE_API.Repository;
using FSE_API.DBMessages;
using FSE_API.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace FSE_API.Service
{
    public class TaskProjectService : ITaskProjectService
    {
        private readonly IProjectTask projectTaskRepo;
        private readonly ILogger<TaskProjectService> logger;
        private readonly IMapper mapper;

        public TaskProjectService(IProjectTask projectTaskRepo,
            ILogger<TaskProjectService> logger, IMapper mapper)
        {
            this.projectTaskRepo = projectTaskRepo;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<Tuple<bool, string>> AddProject(AddProject project)
        {
            if ((project.EndDate > DateTime.MinValue) && (project.StartDate > DateTime.MinValue)
                    && (project.StartDate > project.EndDate))
                return new Tuple<bool, string>(false, "start date greater than end date");
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(project);
            var validationResults = new List<ValidationResult>();
            if (Validator.TryValidateObject(project, validationContext, validationResults))
            {
                var projectDO = mapper.Map<Project>(project);
                return await projectTaskRepo.AddProject(projectDO);
            }
            return new Tuple<bool, string>(false, "validation failures");
        }

        public async Task<Tuple<bool, string>> EditProject(ModelProject project)
        {
            if ((project.EndDate > DateTime.MinValue) && (project.StartDate > DateTime.MinValue)
                    && (project.StartDate > project.EndDate))
                return new Tuple<bool, string>(false, "start date greater than end date");
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(project);
            var validationResults = new List<ValidationResult>();
            if (Validator.TryValidateObject(project, validationContext, validationResults))
            {
                var projectDO = mapper.Map<Project>(project);
                return await projectTaskRepo.EditProject(projectDO);
            }
            return new Tuple<bool, string>(false, "validation failures");
        }

        public async Task<List<ListProject>> GetAllActiveProject()
        {
            var projUserVO = await projectTaskRepo.GetAllActiveProject();
            var results = mapper.Map<List<ListProject>>(projUserVO);
            return results;
        }



        public async Task<List<ListProject>> GetProjectByName(string projectName)
        {
            var projUserVO = await projectTaskRepo.GetProjectByName(projectName);
            var results = mapper.Map<List<ListProject>>(projUserVO);
            return results;
        }

        public async Task<bool> SuspendProject(string projectId)
        {
            return await projectTaskRepo.SuspendProject(projectId);
        }

        public async Task<List<ListTask>> GetAllActiveTask(string projectId)
        {
            var taskUservo = await projectTaskRepo.GetAllActiveTask(projectId);
            if (taskUservo.Any())
            {
                var parentTaskIds = taskUservo?.Select<UserTaskVO, string>(vo => vo?.Tasks?.ParentTaskId)?.Distinct()?
                                               .Where(Parid => (!string.IsNullOrWhiteSpace(Parid))).ToList();
                List<TaskProject> parentTasklst = null;
                if (parentTaskIds.Any())
                    parentTasklst = taskUservo?.Select<UserTaskVO, TaskProject>(vo => vo?.Tasks)
                                               .Where(task => parentTaskIds.Contains(task.Id))
                                               .ToList();
                var result = new List<ListTask>();
                taskUservo.ForEach((item) =>
                {
                    ListTask projectListing = new ListTask
                    {
                        EndDate = ((item.Tasks.EndDate == DateTime.MinValue) && (!string.IsNullOrWhiteSpace(item.Tasks?.ParentTaskId))) ?
                                   (parentTasklst.FirstOrDefault(tsk =>
                                   tsk.Id == item.Tasks?.ParentTaskId)?.EndDate).Value : item.Tasks.EndDate,
                        Priority = (item.Tasks?.Priority).Value,
                        ParentTaskId = item.Tasks?.ParentTaskId,
                        ParentDescription = (string.IsNullOrWhiteSpace(item.Tasks?.ParentTaskId)) ?
                                            "No Parent Task" : parentTasklst.FirstOrDefault(tsk =>
                                                               tsk.Id == item.Tasks?.ParentTaskId)?.Name,
                        ProjectId = item.ProjectId,
                        StartDate = ((item.Tasks.Start == DateTime.MinValue) && (!string.IsNullOrWhiteSpace(item.Tasks?.ParentTaskId))) ?
                                   (parentTasklst.FirstOrDefault(tsk =>
                                   tsk.Id == item.Tasks?.ParentTaskId)?.Start).Value : item.Tasks.Start,
                        Status = item.Tasks.Status,
                        TaskOwnerId = item.Tasks.TaskOwnerId,
                        TaskDescription = item.Tasks.Name,
                        TaskId = item.Tasks.Id,
                        TaskOwnerName = item.UserName
                    };
                    result.Add(projectListing);
                });
                return result;
            }
            return null;
        }

        public async Task<Tuple<bool, string>> AddTask(AddTask projTask)
        {
            if ((projTask.EndDate > DateTime.MinValue) && (projTask.StartDate > DateTime.MinValue)
                   && (projTask.StartDate > projTask.EndDate))
                return new Tuple<bool, string>(false, "start date greater than end date");
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(projTask);
            var validationResults = new List<ValidationResult>();
            if (Validator.TryValidateObject(projTask, validationContext, validationResults))
            {
                var taskDO = mapper.Map<TaskProject>(projTask);
                return await projectTaskRepo.AddTask(projTask.ProjectId, taskDO);
            }
            return new Tuple<bool, string>(false, "validation failures");
        }

        public async Task<Tuple<bool, string>> EditTask(TaskModal projTask)
        {
            if ((projTask.EndDate > DateTime.MinValue) && (projTask.StartDate > DateTime.MinValue)
                   && (projTask.StartDate > projTask.EndDate))
                return new Tuple<bool, string>(false, "start date greater than end date");
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(projTask);
            var validationResults = new List<ValidationResult>();
            if (Validator.TryValidateObject(projTask, validationContext, validationResults))
            {
                var taskDO = mapper.Map<TaskProject>(projTask);
                return await projectTaskRepo.EditTask(projTask.ProjectId, taskDO);
            }
            return new Tuple<bool, string>(false, "validation failures");
        }

        public async Task<Tuple<bool, string>> EndTask(string projectId, string taskId)
        {
            var projTasks = await projectTaskRepo.GetAllActiveTask(projectId);
            var parentTaskIds = projTasks?.Select<UserTaskVO, string>(vo => vo?.Tasks?.ParentTaskId)?
                                               .Where(Parid => (taskId.CompareTo(Parid) == 0)).ToList();
            if (parentTaskIds.Any())
                throw new ApplicationException("cannot close this task as there are some open child task");
            return await projectTaskRepo.EndTask(projectId, taskId);
        }

    }
}
