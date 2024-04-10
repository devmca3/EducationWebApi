using System;
namespace EducationWebApi.Classes.Param
{
	public class CategoryImageParam
    {
        public int? CategoryImageMapId { get; set; }
        public int? CategoryID { get; set; }
        public long? ImageId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
    public class CategoryImageSlideParam
    {
        public int? CategoryImgSlideMapId { get; set; }
        public int? CategoryId { get; set; }
        public long? ImageId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}

