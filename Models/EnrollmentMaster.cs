using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class EnrollmentMaster
{
    public int EnrollmentId { get; set; }

    public int? EnrollmentNo { get; set; }

    public int? TrainingCenterId { get; set; }

    public int? CourseId { get; set; }

    public int? CycleId { get; set; }

    public int? StudentId { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public string? CancellationReason { get; set; }
}
