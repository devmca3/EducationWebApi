using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class SubjectPointStepMaster
{
    public int SubjectStepId { get; set; }

    public int? SubjectPointId { get; set; }

    public int? SubjectId { get; set; }

    public string? SubjectStepName { get; set; }

    public long? SubjectStepImageId { get; set; }

    public string? SubjectStepDescription { get; set; }

    public int? SubjectStepVideoId { get; set; }

    public int? OrderNo { get; set; }

    public bool? IsActive { get; set; }
}
