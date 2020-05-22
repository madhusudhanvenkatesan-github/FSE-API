using System;
namespace FSE_API.Model
{
    public class TaskProject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentTaskId { get; set; }
        public int Priority { get; set; }
        public DateTime Start { get; set; }
        public DateTime EndDate { get; set; }
        public string TaskOwnerId { get; set; }
        public int Status { get; set; }
    }
}
