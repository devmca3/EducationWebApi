using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EducationWebApi.Classes.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public FileController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        
       
        [HttpGet]
        public string GetImagebase64(string ndate,string filename)
        {   
            string data = ndate;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images/" + filename);
            byte[] b = System.IO.File.ReadAllBytes(filePath);
            return "data:image/png;base64," + Convert.ToBase64String(b);
        }

        

        [HttpGet]
        public async Task<IActionResult> GetImageFile(string ndate,string filename)
        {
            string data = ndate;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images/" + filename);
            if (!System.IO.File.Exists(filePath))
                return NotFound();
            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), filePath);
        }
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}

