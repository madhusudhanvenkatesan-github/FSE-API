using System.ComponentModel.DataAnnotations;

namespace FSE_API.DBMessages
{
    public class AddUserMsg
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
