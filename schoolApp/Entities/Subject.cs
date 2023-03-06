using System;
using System.Collections.Generic;

namespace schoolApp.Entities;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public virtual ICollection<StudentSubject> StudentSubjects { get; } = new List<StudentSubject>();
}
