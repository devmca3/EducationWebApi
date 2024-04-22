    public class UploadImageParam
    {
        public long? Id { get; set; }

        public long? ImageId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }

     public class UploadImageParamType
    {
        public long? Id { get; set; }

        public long? ImageId { get; set; }
        public long? TypeId { get; set; }

        public IFormFile? ImageFile { get; set; }

    }

    public class UploadImageMapParam
    {
        public long? MapId { get; set; }

        public long? Id { get; set; }

        public long? ImageId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }

