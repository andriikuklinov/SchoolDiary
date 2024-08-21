using System;
using System.Collections.Generic;

namespace SchoolDiary.DAL;

public partial class Teacher
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? Birthdate { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
