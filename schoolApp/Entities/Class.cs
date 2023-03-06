using System;
using System.Collections.Generic;

namespace schoolApp.Entities;

public partial class Class
{
    public int ClassId { get; set; }

    public string? ClassName { get; set; }

    public virtual ICollection<StudentDetail> StudentDetails { get; } = new List<StudentDetail>();
}
