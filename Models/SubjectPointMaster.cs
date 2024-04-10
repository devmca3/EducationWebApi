using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class SubjectPointMaster
{
    public int SubjectPointId { get; set; }

    public int? SubjectId { get; set; }

    public string? SubjectPointName { get; set; }

    public string? SubjectDescription { get; set; }

    public long? SubjectImageId { get; set; }

    public long? VideoId { get; set; }

    public bool? IsActive { get; set; }

    public int? OrderNo { get; set; }
}
