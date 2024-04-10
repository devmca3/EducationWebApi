using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class VideoMaster
{
    public long VideoId { get; set; }

    public string? VideoUrl { get; set; }

    public bool? IsActive { get; set; }
}
