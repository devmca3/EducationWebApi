using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class SubjectMaster
{
    public int SubjectId { get; set; }

    public string? SubjectName { get; set; }

    public int? CourseId { get; set; }

    public long? ImageIdsm { get; set; }

    public long? ImageIdlg { get; set; }

    public bool? IsActive { get; set; }

    public virtual CourseMaster? Course { get; set; }

    public virtual ICollection<SubjectPointMaster> SubjectPointMasters { get; set; } = new List<SubjectPointMaster>();

    public virtual ICollection<SubjectPointStepMaster> SubjectPointStepMasters { get; set; } = new List<SubjectPointStepMaster>();
}
