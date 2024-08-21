using System;
using System.Collections.Generic;

namespace SchoolDiary.DAL;

public partial class Class
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int GroupId { get; set; }

    public int TeacherId { get; set; }

    public int SubjectId { get; set; }

    public int ClassRoomId { get; set; }

    public int ClassPeriodId { get; set; }

    public int TermId { get; set; }

    public virtual ClassPeriod ClassPeriod { get; set; } = null!;

    public virtual ClassRoom ClassRoom { get; set; } = null!;

    public virtual ICollection<ClassScore> ClassScores { get; set; } = new List<ClassScore>();

    public virtual Group Group { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;

    public virtual Term Term { get; set; } = null!;
}
