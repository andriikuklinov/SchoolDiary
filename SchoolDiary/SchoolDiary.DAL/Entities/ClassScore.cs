using System;
using System.Collections.Generic;

namespace SchoolDiary.DAL.Entities;

public partial class ClassScore
{
    public int Id { get; set; }

    public int? Score { get; set; }

    public DateOnly? ScoreDate { get; set; }

    public int ClassId { get; set; }

    public int StudentId { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
