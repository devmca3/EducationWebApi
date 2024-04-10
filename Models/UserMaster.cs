using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class UserMaster
{
    public long UserId { get; set; }

    public string? Name { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public int? MobileNo { get; set; }

    public int? UserTypeId { get; set; }

    public bool? IsActive { get; set; }
}
