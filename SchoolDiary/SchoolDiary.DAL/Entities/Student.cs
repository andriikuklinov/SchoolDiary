using System;
using System.Collections.Generic;

namespace SchoolDiary.DAL.Entities;

public partial class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string? Email { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public DateOnly? EnrolnmentDate { get; set; }

    public int GroupId { get; set; }

    public virtual ICollection<ClassScore> ClassScores { get; set; } = new List<ClassScore>();

    public virtual Group Group { get; set; } = null!;
}
