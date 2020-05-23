using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSE.API.Messages
{
    public class UserSearchCriteria
    {
        public string EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
