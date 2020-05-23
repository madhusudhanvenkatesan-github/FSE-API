using FSE.API.DomainModel;
using FSE.API.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSE.API.DomainService
{
    public interface IUserService
    {
        Task<Tuple<bool, string>> Add(UserAddMsg userAdd);
        Task<Tuple<bool, string>> Edit(UserModMsg userMod);
        Task<List<PMUser>> GetAllUser();
        Task<List<PMUser>> GetUserByCriteria(UserSearchCriteria userSearchCriteria);
        Task<PMUser> GetUserByEmployeeId(string employeeId);
        Task<bool> Delete(string EmployeeId);


    }
}
