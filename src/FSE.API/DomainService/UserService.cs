using Microsoft.Extensions.Logging;
using FSE.API.DomainModel;
using FSE.API.Messages;
using FSE.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace FSE.API.DomainService
{
    public class UserService:IUserService
    {
        private readonly IUserRepo userRepo;
        private readonly IProjectTaskRepo projectTaskRepo;
        private readonly ILogger<UserService> usLogger;
        private readonly IMapper mapper;
        public UserService(IUserRepo userRepo, IProjectTaskRepo projectTaskRepo, ILogger<UserService> logger,
            IMapper mapper)
        {
            this.userRepo = userRepo;
            this.projectTaskRepo = projectTaskRepo;
            usLogger = logger;
            this.mapper = mapper;
        }

        public async Task<Tuple<bool, string>> Add(UserAddMsg userAdd)
        {
            
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(userAdd);
            var validationResults = new List<ValidationResult>();
            if (Validator.TryValidateObject(userAdd, validationContext, validationResults))
            {
                var pmUser = mapper.Map<PMUser>(userAdd);
                return await userRepo.AddUser(pmUser);
            }
            return new Tuple<bool, string>(false, "");
        }

        public async Task<bool> Delete(string EmployeeId)
        {
            var pmUser = await userRepo.GetUserByEmployeeId(EmployeeId);
            var projUserCount = await projectTaskRepo.GetProjectCountByPM(pmUser.Id);
            if (projUserCount > 0)
                throw new ApplicationException("Cannot Delete. User asociated with one more projects");
            var tskUserCount = await projectTaskRepo.GetTaskCountByUser(pmUser.Id);
            if (tskUserCount >0 )
                throw new ApplicationException("Cannot Delete. User asociated with one more tasks");
            return await userRepo.DeleteUser(pmUser);


        }

        public async  Task<Tuple<bool, string>> Edit(UserModMsg userMod)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(userMod);
            var validationResults = new List<ValidationResult>();
            if (Validator.TryValidateObject(userMod, validationContext, validationResults))
            {
                var pmUser = mapper.Map<PMUser>(userMod);
                return await userRepo.EditUser(pmUser);
            }
            return new Tuple<bool, string>(false, "");

        }

        public async Task<List<PMUser>> GetAllUser()
        {
            return await userRepo.GetAllUser();
        }

        public async Task<List<PMUser>> GetUserByCriteria(UserSearchCriteria userSearchCriteria)
        {
            return await userRepo.GetAllUserMatchAnyCriteria(userSearchCriteria);
        }

        public async Task<PMUser> GetUserByEmployeeId(string employeeId)
        {
            return await userRepo.GetUserByEmployeeId(employeeId);
        }
    }
}
