using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSE.API.DomainModel
{
    public class TaskUserVO
    {
        public ProjTask Tasks { get; set; }
        public string UserName { get; set; }
        public string ProjectId { get; set; } 

    }
}
