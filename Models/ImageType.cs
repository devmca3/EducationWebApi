using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class ImageType
{
    public int ImageTypeId { get; set; }

    public string ImageType1 { get; set; } = null!;

    public virtual ICollection<ImageMaster> ImageMasters { get; set; } = new List<ImageMaster>();
}
