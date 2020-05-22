using Microsoft.Extensions.Logging;
using FSE_API.Model;
using FSE_API.DBMessages;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;

namespace FSE_API.Repository
{
    public class User : IUser
    {
        private IAsyncDocumentSession asyncDocumentSession;
        private ILogger<User> logger;
        public User(IAsyncDocumentSession asyncDocumentSession,
            ILogger<User> logger)
        {
            this.asyncDocumentSession = asyncDocumentSession;
            this.logger = logger;
        }
        public async Task<Tuple<bool, string>> AddUser(PMUser pmUser)
        {

            try
            {
                await asyncDocumentSession.StoreAsync(pmUser);
                await asyncDocumentSession.SaveChangesAsync();
                return new Tuple<bool, string>(true, pmUser.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteUser(PMUser pmUser)
        {
            var dbPmoUser = await asyncDocumentSession.Query<PMUser>()
                                                      .Where(user => user.EmployeeId == pmUser.EmployeeId)
                                                      .FirstOrDefaultAsync();
            try
            {
                asyncDocumentSession.Delete<PMUser>(pmUser);
                await asyncDocumentSession.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Tuple<bool, string>> EditUser(PMUser pmUser)
        {
            var dbPmoUser = await asyncDocumentSession.Query<PMUser>()
                                                      .Where(user => user.EmployeeId == pmUser.EmployeeId)
                                                      .FirstOrDefaultAsync();
            if (dbPmoUser == default)
                throw new ApplicationException("Provided user is not yet added in DB");
            dbPmoUser.EmployeeId = pmUser.EmployeeId;
            dbPmoUser.FirstName = pmUser.FirstName;
            dbPmoUser.LastName = pmUser.LastName;
            try
            {
                await asyncDocumentSession.SaveChangesAsync();
                return new Tuple<bool, string>(true, dbPmoUser.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<PMUser>> GetAllUser()
        {
            return await asyncDocumentSession.Query<PMUser>().ToListAsync();
        }

        public async Task<List<PMUser>> GetAllUserMatchAnyCriteria(userSearchCriteria userSearchCriteria)
        {
            var predicate = PredicateBuilder.New<PMUser>(false);
            if (!string.IsNullOrWhiteSpace(userSearchCriteria.EmployeeID))
                predicate = predicate.Or(user => user.EmployeeId == userSearchCriteria.EmployeeID);
            if (!string.IsNullOrWhiteSpace(userSearchCriteria.FirstName))
                predicate = predicate.Or(user => user.FirstName == userSearchCriteria.FirstName);
            if (!string.IsNullOrWhiteSpace(userSearchCriteria.LastName))
                predicate = predicate.Or(user => user.LastName == userSearchCriteria.LastName);
            return await asyncDocumentSession.Query<PMUser>()
                                             .Where(predicate)
                                             .ToListAsync();

        }

        public async Task<PMUser> GetUserByEmployeeId(string employeeId)
        {
            return await asyncDocumentSession.Query<PMUser>()
                                             .Where(user => user.EmployeeId == employeeId)
                                             .FirstOrDefaultAsync();
        }
    }
}
