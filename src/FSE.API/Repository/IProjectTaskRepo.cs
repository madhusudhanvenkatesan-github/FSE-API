using FSE.API.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSE.API.Repository
{
    public interface IProjectTaskRepo
    {
        Task<int> GetProjectCountByPM(string pmId);
        Task<int> GetTaskCountByUser(string userId);
        Task<List<ProjectUserVO>> GetAllActiveProject();
        Task<List<TaskUserVO>> GetAllActiveTask(string projectId);
        Task<List<ProjectUserVO>> GetProjectByName(string projectName);
        Task<Tuple<bool, string>> AddProject(Project project);
        Task<Tuple<bool, string>> EditProject(Project project);
        Task<bool> SuspendProject(string ProjectId);
        Task<Tuple<bool, string>> AddTask(string projectId, ProjTask projTask);
        Task<Tuple<bool, string>> EditTask(string projectId, ProjTask projTask);
        Task<Tuple<bool, string>> EndTask(string projectId, string taskId);
    }
}
