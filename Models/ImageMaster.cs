using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class ImageMaster
{
    public long ImageId { get; set; }

    public string? ImageName { get; set; }

    public string Extension { get; set; } = null!;

    public DateTime ImageDate { get; set; }

    public int ImageTypeId { get; set; }

    public bool? IsActive { get; set; }

    public virtual ImageType ImageType { get; set; } = null!;
}
