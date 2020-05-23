using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSE.API.Messages
{
    public class ProjectListing
    {
        
        public string ProjId { get; set; }
        
        public string ProjectTitle { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        
        public string PMUsrId { get; set; }
        public string PMUsrName { get; set; }
        public int TotalTaskCount { get; set; }
        public int CompletedTaskCount { get; set; }
        public int Priority { get; set; }

    }
}
