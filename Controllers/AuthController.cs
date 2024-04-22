using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EducationWebApi.Classes.DTO;
using EducationWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly db_Context _context;
        DtoReturnData RD = null;
        public AuthController(IConfiguration configuration, db_Context context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPost]
        public async Task<DtoReturnData> Login([FromBody] UserDto user)
        {
            string username = user.Username.Trim('\'').Trim('=').Trim(' ');
            string password = user.Password.Trim('\'').Trim('=').Trim(' ');
            UserMaster userMaster = null;
            try
            {
                userMaster = _context.UserMasters.Where(x => x.UserName == username && x.Password == password).First<UserMaster>();
            }
            catch (Exception ex)
            {
                userMaster = null;
            }
            

            string token = null;
            token = userMaster != null ? CreateToken(userMaster) : null;
            if (token != null)
            {
                RD = new DtoReturnData
                {
                    Data = token,
                    Message = "Success",
                    Status = true,
                    OtherData = "",
                    TotalCount = 0,
                    HttpStatus = HttpStatusCode.OK
                };
            }
            else
            {
                RD = new DtoReturnData
                {
                    Data = "",
                    Message = "Fail",
                    Status = false,
                    OtherData = "",
                    TotalCount = 0,
                    HttpStatus = HttpStatusCode.OK
                };
            }
            return await Task.FromResult(RD);
        }
        private string CreateToken(UserMaster user)
        {

            List<Claim> claims = new List<Claim> {
                new Claim("Name", user.Name),
                new Claim("Role", user.UserTypeId.ToString()),
                new Claim(ClaimTypes.Role, user.UserTypeId.ToString()),
                new Claim("Uid", user.UserId.ToString())
                //new Claim("MobileNo", user..ToString()),
                //new Claim("ExpiryDate", user.ExpiryDate.ToString("yyyy-MM-dd")),
                //new Claim("IsActive", user.isac.ToString()),
                //new Claim("Status", user.Status.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ajlfjldfl745747dfdfdjkhfjfdljfljlfflfdljlfjderirrieiru4857398457943857djfdsjhfsdfhkdshfksdhfkhsdakfhkdshfhsdjkfhdshfdshfdfdslfldsfldhsfhdskfhkdshvnvnjdkhfsdfhsdhfdhsjkfhskdhfkahhdjkh4y4389578943575497nvbvcfsfqhk"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetUserDetail()
        {
            DtoUserDetail dtoUserDetail = null;
            //var data = User.Claims.ToList();
            string name = User.Claims.First(x => x.Type == "Name").Value.ToString();
            //string email = User.Claims.First(x => x.Type == "EmailID").Value.ToString();
            //string mobile = User.Claims.First(x => x.Type == "MobileNo").Value.ToString();
            string role = User.Claims.First(x => x.Type == ClaimTypes.Role).Value.ToString();
            //string companyid = User.Claims.First(x => x.Type == "CompanyID").Value.ToString();
            string Uid = User.Claims.First(x => x.Type == "Uid").Value.ToString();
            //string expirydate = User.Claims.First(x => x.Type == "ExpiryDate").Value.ToString();
            //string isactive = User.Claims.First(x => x.Type == "IsActive").Value.ToString();
            //string status = User.Claims.First(x => x.Type == "Status").Value.ToString();
            dtoUserDetail = new DtoUserDetail()
            {
                Name = name,
                //EmailID = email,
                //MobileNo = mobile,
                Role = role,
                //CompanyID = Convert.ToInt32(companyid.ToString()),
                Uid = Convert.ToInt32(Uid)
                /*
                ExpiryDate = Convert.ToDateTime(expirydate),
                IsActive = Convert.ToBoolean(isactive),
                Status = Convert.ToBoolean(status)
                */
            };
            //var userName = User?.Identity?.Name;
            //var roleClaims = User?.FindAll(ClaimTypes.Role);
            //var roles = roleClaims?.Select(c => c.Value).ToList();
            //var roles2 = User?.Claims
            //    .Where(c => c.Type == ClaimTypes.Role)
            //    .Select(c => c.Value)
            //    .ToList();
            //return Ok(new { userName, roles, roles2 });
            //var jti = tokenS.Claims.First(claim => claim.Type == "jti").Value;
            RD = new DtoReturnData
            {
                Data = dtoUserDetail,
                Message = "Success",
                Status = true,
                OtherData = "",
                TotalCount = 0,
                HttpStatus = HttpStatusCode.OK
            };

            return Ok(RD);
        }
        private bool UserMasterExists(int id)
        {
            return (_context.UserMasters?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

    }
}

