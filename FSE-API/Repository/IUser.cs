using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FSE_API.DBMessages;
using FSE_API.Model;

namespace FSE_API.Repository
{
    public interface IUser
    {
        public Task<Tuple<bool, string>> AddUser(PMUser pmUser);
        public Task<Tuple<bool, string>> EditUser(PMUser pmUser);
        public Task<List<PMUser>> GetAllUser();
        public Task<List<PMUser>> GetAllUserMatchAnyCriteria(userSearchCriteria userSearchCriteria);
        public Task<PMUser> GetUserByEmployeeId(string employeeId);
        public Task<bool> DeleteUser(PMUser pmUser);
    }
}
