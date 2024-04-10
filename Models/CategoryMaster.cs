using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class CategoryMaster
{
    public int CategoryId { get; set; }

    public int? ParentCategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? ShortDescription { get; set; }

    public string? FullDescription { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoKeyword { get; set; }

    public string? SeoDescription { get; set; }

    public string? SeoUrl { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<CategoryImageMap> CategoryImageMaps { get; set; } = new List<CategoryImageMap>();

    public virtual ICollection<CategoryImageSlideMap> CategoryImageSlideMaps { get; set; } = new List<CategoryImageSlideMap>();

    public virtual ICollection<CategoryVideoMap> CategoryVideoMaps { get; set; } = new List<CategoryVideoMap>();
}
