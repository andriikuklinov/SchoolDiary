using System;
using System.Collections.Generic;

namespace SchoolDiary.DAL.Entities;

public partial class Group
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
