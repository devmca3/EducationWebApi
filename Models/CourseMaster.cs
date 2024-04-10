using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class CourseMaster
{
    public int CourseId { get; set; }

    public string? CourseName { get; set; }

    public string? CourseDescription { get; set; }

    public decimal? CourseFee { get; set; }

    public long? CourseImageSm { get; set; }

    public long? CourseImageLg { get; set; }

    public bool? Status { get; set; }

    public bool? Deleted { get; set; }
}
