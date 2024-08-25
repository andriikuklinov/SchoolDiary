using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.WEB.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        public string Email { get; set; }
    }
}
