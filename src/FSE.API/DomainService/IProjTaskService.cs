using FSE.API.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSE.API.DomainService
{
   public interface IProjTaskService
    {
        Task<List<ProjectListing>> GetAllActiveProject();
        Task<List<ProjectListing>> GetProjectByName(string projectName);
        Task<Tuple<bool, string>> AddProject(ProjectAdd project);
        Task<Tuple<bool, string>> EditProject(ProjectMod project);
        Task<bool> SuspendProject(string projectId);
        Task<List<TaskListing>> GetAllActiveTask(string projectId);
        Task<Tuple<bool, string>> AddTask(TaskAdd projTask);
        Task<Tuple<bool, string>> EditTask(TaskMod projTask);
        Task<Tuple<bool, string>> EndTask(string projectId, string taskId);
    }
}
