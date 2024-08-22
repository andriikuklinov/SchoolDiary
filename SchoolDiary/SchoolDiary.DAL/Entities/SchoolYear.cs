using System;
using System.Collections.Generic;

namespace SchoolDiary.DAL.Entities;

public partial class SchoolYear
{
    public int Id { get; set; }

    public string? YearName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ICollection<Term> Terms { get; set; } = new List<Term>();
}
