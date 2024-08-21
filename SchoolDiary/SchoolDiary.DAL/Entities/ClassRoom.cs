using System;
using System.Collections.Generic;

namespace SchoolDiary.DAL.Entities;

public partial class ClassRoom
{
    public int Id { get; set; }

    public string? ClassName { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
