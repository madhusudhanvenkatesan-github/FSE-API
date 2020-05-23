using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSE.API.Messages
{
    public class TaskListing
    {
        public string ProjectId { get; set; }
        public string TaskId { get; set; }
        public string TaskDescription { get; set; }
        public string  ParentTaskId { get; set; }
        public string ParentDescription { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TaskOwnerId { get; set; }
        public string TaskOwnerName { get; set; }
    }
}
