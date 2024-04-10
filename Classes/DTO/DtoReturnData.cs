using System;
using System.Net;

namespace EducationWebApi.Classes.DTO
{
	public class DtoReturnData
    {
        public bool Status { get; set; }

        public HttpStatusCode HttpStatus { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public long TotalCount { get; set; }
        public dynamic OtherData { get; set; }
    }
}

