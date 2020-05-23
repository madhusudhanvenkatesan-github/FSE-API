using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FSE.API.Messages
{
    public class ProjectAdd
    {
        [Required]
        public string ProjectTitle { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        [Required]
        public string PMUsrId { get; set; }
        public int Priority { get; set; }
    }
}
