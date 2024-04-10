using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class CategoryImageSlideMap
{
    public int CategoryImgSlideMapId { get; set; }

    public int? CategoryId { get; set; }

    public long? ImageId { get; set; }

    public virtual CategoryMaster? Category { get; set; }

    public virtual ImageMaster? Image { get; set; }
}
