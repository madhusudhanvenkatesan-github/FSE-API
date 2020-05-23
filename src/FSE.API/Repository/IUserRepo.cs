using FSE.API.DomainModel;
using FSE.API.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSE.API.Repository
{
    public interface IUserRepo
    {
        public Task<Tuple<bool, string>> AddUser(PMUser pmUser);
        public Task<Tuple<bool, string>> EditUser(PMUser pmUser);
        public Task<List<PMUser>> GetAllUser();
        public Task<List<PMUser>> GetAllUserMatchAnyCriteria(UserSearchCriteria userSearchCriteria);
        public Task<PMUser> GetUserByEmployeeId(string employeeId);
        public Task<bool> DeleteUser(PMUser pmUser);
    }
}
