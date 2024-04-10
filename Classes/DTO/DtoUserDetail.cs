using System;
namespace EducationWebApi.Classes.DTO
{
    public class DtoUserDetail
    {
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public string Role { get; set; }
        public int Uid { get; set; }
        public int CompanyID { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean Status { get; set; }
    }
}

