using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class StudentMaster
{
    public int StudentId { get; set; }

    public string? StudentName { get; set; }

    public string? EmailId { get; set; }

    public int? PhoneNo { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }
}
