using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class SlideMaster
{
    public int SlideId { get; set; }

    public string? SlideName { get; set; }

    public long? ImageId { get; set; }

    public int? OrderNo { get; set; }

    public bool? IsActive { get; set; }
}
