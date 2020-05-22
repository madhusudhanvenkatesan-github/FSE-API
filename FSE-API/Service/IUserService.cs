using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FSE_API.DBMessages;
using FSE_API.Model;

namespace FSE_API.Controllers
{
    public interface IUserService
    {
        Task<Tuple<bool, string>> Add(AddUserMsg userAdd);
        Task<Tuple<bool, string>> Edit(ModelUserMsg userMod);
        Task<List<PMUser>> GetAllUser();
        Task<List<PMUser>> GetUserByCriteria(userSearchCriteria userSearchCriteria);
        Task<PMUser> GetUserByEmployeeId(string employeeId);
        Task<bool> Delete(string EmployeeId);
    }
}