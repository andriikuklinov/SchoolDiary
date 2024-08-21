using System;
using System.Collections.Generic;

namespace SchoolDiary.DAL;

public partial class Subject
{
    public int Id { get; set; }

    public string SubjectName { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
