using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class CouseImageMap
{
    public int CourseImageId { get; set; }

    public int? CourseId { get; set; }

    public long? ImageId { get; set; }
}
