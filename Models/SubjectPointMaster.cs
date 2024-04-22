using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class SubjectPointMaster
{
    public int SubjectPointId { get; set; }

    public int? SubjectId { get; set; }

    public string? SubjectPointName { get; set; }

    public string? SubjectPointDescription { get; set; }

    public long? SubjectPointImageIdlg { get; set; }

    public long? SubjectPointImageIdsm { get; set; }

    public string? SubjectPointVideoUrl { get; set; }

    public bool? IsActive { get; set; }

    public int? OrderNo { get; set; }

    public virtual SubjectMaster? Subject { get; set; }

    public virtual ICollection<SubjectPointStepMaster> SubjectPointStepMasters { get; set; } = new List<SubjectPointStepMaster>();
}
