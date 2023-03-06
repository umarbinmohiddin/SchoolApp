using System;
using System.Collections.Generic;

namespace schoolApp.Entities;

public partial class StudentDetail
{
    public int StudentId { get; set; }

    public string? FName { get; set; }

    public string? LName { get; set; }

    public int? ClassId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<StudentSubject> StudentSubjects { get; } = new List<StudentSubject>();

    public string FullName
    {
        get
        {
            return FName + " " + LName;
        }
    }
}
