using System;
using System.Collections.Generic;

namespace SchoolDiary.DAL;

public partial class ClassPeriod
{
    public int Id { get; set; }

    public byte? DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
