using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class CycleMaster
{
    public int CycleId { get; set; }

    public string? CycleDescription { get; set; }

    public DateTime? CycleCreateDate { get; set; }

    public DateTime? CycleStartDate { get; set; }

    public DateTime? CycleEndDate { get; set; }

    public bool? IsActive { get; set; }
}
