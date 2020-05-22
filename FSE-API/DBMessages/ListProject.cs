using System;
namespace FSE_API.DBMessages
{
    public class ListProject
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
