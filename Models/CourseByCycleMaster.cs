using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class CourseByCycleMaster
{
    public int CourseByCycleId { get; set; }

    public int? CourseId { get; set; }

    public int? CycleId { get; set; }

    public DateTime? CourseStartDate { get; set; }

    public DateTime? CourseEndDate { get; set; }

    public bool? IsActive { get; set; }
}
