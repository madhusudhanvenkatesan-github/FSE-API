using System;
using System.ComponentModel.DataAnnotations;

namespace FSE_API.DBMessages
{
    public class ModifyUserMsg
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
