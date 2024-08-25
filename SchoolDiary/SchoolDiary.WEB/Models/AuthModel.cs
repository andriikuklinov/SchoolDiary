using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.WEB.Models
{
    public class AuthModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
