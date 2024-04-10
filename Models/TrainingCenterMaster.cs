using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class TrainingCenterMaster
{
    public int CenterId { get; set; }

    public string? CenterName { get; set; }

    public string? EmailId { get; set; }

    public int? MobileNo { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }

    public int? UserId { get; set; }

    public bool? IsActive { get; set; }
}
