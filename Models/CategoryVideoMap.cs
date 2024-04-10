using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class CategoryVideoMap
{
    public long CategoryVideoMapId { get; set; }

    public int? CategoryId { get; set; }

    public long? VideoId { get; set; }

    public virtual CategoryMaster? Category { get; set; }

    public virtual VideoMaster? Video { get; set; }
}
