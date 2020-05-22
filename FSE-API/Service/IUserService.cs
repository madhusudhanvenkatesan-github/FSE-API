using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FSE_API.DBMessages;
using FSE_API.Model;

namespace FSE_API.Controllers
{
    public interface IUserService
    {
        Task<Tuple<bool, string>> Add(UserAddMsg userAdd);
        Task<Tuple<bool, string>> Edit(UserModMsg userMod);
        Task<List<PMOUser>> GetAllUser();
        Task<List<PMOUser>> GetUserByCriteria(UserSearchCriteria userSearchCriteria);
        Task<PMOUser> GetUserByEmployeeId(string employeeId);
        Task<bool> Delete(string EmployeeId);
    }
}