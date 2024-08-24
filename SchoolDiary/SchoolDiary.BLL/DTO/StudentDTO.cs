using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.BLL.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string MiddleName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public DateTime Birthdate { get; set; }
        public DateTime EnrolnmentDate { get; set; }
        public int GroupId { get; set; }
    }
}
