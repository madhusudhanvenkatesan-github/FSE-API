using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSE_API.Model;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace FSE_API.Repository
{
    public class ProjectTask : IProjectTask
    {
        private IAsyncDocumentSession asyncDocumentSession;
        private ILogger<ProjectTask> logger;
        public ProjectTask(IAsyncDocumentSession asyncDocumentSession, ILogger<ProjectTask> logger)
        {
            this.asyncDocumentSession = asyncDocumentSession;
            this.logger = logger;
        }

        public async Task<Tuple<bool, string>> AddProject(Project project)
        {
            try
            {
                await asyncDocumentSession.StoreAsync(project);
                await asyncDocumentSession.SaveChangesAsync();
                return new Tuple<bool, string>(true, project.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Tuple<bool, string>> AddTask(string projectId, TaskProject projTask)
        {
            try
            {
                var project = await asyncDocumentSession.LoadAsync<Project>(projectId);
                project.MaxTaskCount++;
                projTask.Id = $@"{project.Id}-{project.MaxTaskCount}";
                project.ProjectTasks.Add(projTask);
                await asyncDocumentSession.SaveChangesAsync();
                return new Tuple<bool, string>(true, projTask.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Tuple<bool, string>> EditProject(Project project)
        {
            try
            {
                var dbProj = await asyncDocumentSession.LoadAsync<Project>(project.Id);
                if (dbProj == null)
                    throw new ApplicationException("Project not found");
                dbProj.PMId = project.PMId;
                dbProj.Priority = project.Priority;
                dbProj.Start = project.Start;
                dbProj.End = project.End;
                dbProj.Title = project.Title;
                await asyncDocumentSession.SaveChangesAsync();
                return new Tuple<bool, string>(true, dbProj.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Tuple<bool, string>> EditTask(string projectId, TaskProject projTask)
        {
            var dbProj = await asyncDocumentSession.LoadAsync<Project>(projectId);

            if (dbProj == null)
                throw new ApplicationException("Project not found");
            var task = dbProj.ProjectTasks.Find(pt => pt.Id == projTask.Id);
            if (task == default)
                throw new ApplicationException("Task not found");
            try
            {
                task.EndDate = projTask.EndDate;
                task.Start = projTask.Start;
                task.Name = projTask.Name;
                task.ParentTaskId = projTask.ParentTaskId;
                task.Priority = projTask.Priority;
                task.TaskOwnerId = projTask.TaskOwnerId;
                await asyncDocumentSession.SaveChangesAsync();
                return new Tuple<bool, string>(true, task.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Tuple<bool, string>> EndTask(string projectId, string taskId)
        {
            var dbProj = await asyncDocumentSession.LoadAsync<Project>(projectId);

            if (dbProj == null)
                throw new ApplicationException("Project not found");
            var task = dbProj.ProjectTasks.Find(pt => pt.Id == taskId);
            if (task == default)
                throw new ApplicationException("Task not found");
            try
            {
                task.EndDate = DateTime.Today;
                task.Status = -1;
                await asyncDocumentSession.SaveChangesAsync();
                return new Tuple<bool, string>(true, taskId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProjectUserVO>> GetAllActiveProject()
        {
            var projects = await asyncDocumentSession.Query<Project>()
                                                    .Include("PMId")
                                                    .Where(proj => proj.Status > -1)
                                                    .ToListAsync();
            var result = new List<ProjectUserVO>();
            foreach (var project in projects)
            {
                var user = await asyncDocumentSession.LoadAsync<PMUser>(project.PMId);
                result.Add(new ProjectUserVO
                {
                    Projects = project,
                    UserName = $"{user.FirstName} {user.LastName}"
                });
            }
            return result;


        }

        public async Task<List<UserTaskVO>> GetAllActiveTask(string projectId)
        {
            var project = await asyncDocumentSession.Include("PMId")
                                                    .LoadAsync<Project>(projectId);
            var tasklst = project.ProjectTasks.Where(pt => pt.Status > -1);
            var result = new List<UserTaskVO>();
            foreach (var task in tasklst)
            {
                var user = await asyncDocumentSession.LoadAsync<PMUser>(task.TaskOwnerId);
                result.Add(new UserTaskVO
                {
                    ProjectId = projectId,
                    UserName = $"{user.FirstName} {user.LastName}",
                    Tasks = task
                });

            }
            return result;
        }

        public async Task<List<ProjectUserVO>> GetProjectByName(string projectName)
        {
            var projects = await asyncDocumentSession.Query<Project>().Include("PMId")
                                       .Search(proj => proj.Title, projectName)
                                       .ToListAsync();
            var result = new List<ProjectUserVO>();
            foreach (var project in projects)
            {
                var user = await asyncDocumentSession.LoadAsync<PMUser>(project.PMId);
                result.Add(new ProjectUserVO
                {
                    Projects = project,
                    UserName = $"{user.FirstName} {user.LastName}"
                });
            }
            return result;
        }

        public async Task<int> GetProjectCountByPM(string pmId)
        {
            return await asyncDocumentSession.Query<Project>().CountAsync(proj => proj.PMId == pmId);
        }

        public async Task<int> GetTaskCountByUser(string userId)
        {
            var projs = await asyncDocumentSession.Query<Project>().Where(projs => projs.Status > -1)
                                                                   .ToListAsync();
            int count = 0;
            foreach (var project in projs)
            {
                count += project.ProjectTasks.Count(tsks => ((tsks.Status > -1) && (tsks.TaskOwnerId == userId)));
            }
            return count;
        }

        public async Task<bool> SuspendProject(string ProjectId)
        {
            var dbProj = await asyncDocumentSession.LoadAsync<Project>(ProjectId);
            if (dbProj == null)
                throw new ApplicationException("Project not found");
            dbProj.End = DateTime.Today;
            dbProj.Status = -1;
            try
            {
                await asyncDocumentSession.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

