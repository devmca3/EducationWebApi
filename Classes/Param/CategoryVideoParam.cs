using System;
namespace EducationWebApi.Classes.Param
{
	public class CategoryVideoParam
	{
        public long CategoryVideoMapId { get; set; }
        public int? CategoryId { get; set; }
        public long? VideoId { get; set; }
        public string? VideoUrl { get; set; }
    }
}

