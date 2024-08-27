using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.WEB.Models
{
    public class UserModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
