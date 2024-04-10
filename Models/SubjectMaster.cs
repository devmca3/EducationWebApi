using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class SubjectMaster
{
    public int SubjectId { get; set; }

    public string? SubjectName { get; set; }

    public long? ImageId { get; set; }

    public int? CourseId { get; set; }

    public bool? IsActive { get; set; }
}
