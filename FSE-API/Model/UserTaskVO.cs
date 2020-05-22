using System;
namespace FSE_API.Model
{
    public class UserTaskVO
    {
        public TaskProject Tasks { get; set; }
        public string UserName { get; set; }
        public string ProjectId { get; set; }
    }
}
