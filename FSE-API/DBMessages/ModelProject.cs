using System;
using System.ComponentModel.DataAnnotations;

namespace FSE_API.DBMessages
{
    public class ModelProject
    {
        [Required]
        public string ProjId { get; set; }

        [Required]
        public string ProjectTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        public string PMUsrId { get; set; }
        public int Priority { get; set; } = 0;
    }
}
