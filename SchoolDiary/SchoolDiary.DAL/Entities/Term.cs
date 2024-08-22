using System;
using System.Collections.Generic;

namespace SchoolDiary.DAL.Entities;

public partial class Term
{
    public int Id { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int YearId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual SchoolYear Year { get; set; } = null!;
}
