using System;
using FSE_API.DBMessages;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace FSE_API.Service
{
    public interface ITaskProjectService
    {
        Task<List<ListProject>> GetAllActiveProject();
        Task<List<ListProject>> GetProjectByName(string projectName);
        Task<Tuple<bool, string>> AddProject(AddProject project);
        Task<Tuple<bool, string>> EditProject(ModelProject project);
        Task<bool> SuspendProject(string projectId);
        Task<List<ListTask>> GetAllActiveTask(string projectId);
        Task<Tuple<bool, string>> AddTask(AddTask projTask);
        Task<Tuple<bool, string>> EditTask(TaskModal projTask);
        Task<Tuple<bool, string>> EndTask(string projectId, string taskId);
    }
}
