using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Threading.Tasks;
using EducationWebApi.Classes.DTO;
using EducationWebApi.Classes.Param;
using EducationWebApi.Models;
using EducationWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
//using NuGet.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
/*
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Key]
*/

namespace EducationWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SlideController : Controller
    {
        public static IWebHostEnvironment _environment;
        private readonly db_Context _context;
        DtoReturnData RD = null;
        public SlideController(IWebHostEnvironment environment,db_Context context)
        {
            _environment = environment;
            _context = context;
        }

        
        [HttpGet]
        public async Task<DtoReturnData> GetSlideMasters(int PageSize,int PageIndex,string? SlideName)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> parameters=new List<SqlParameter>();
            //string procedureName = "dbo.SP_GetSlideMasterList @PageSize, @PageIndex, @TotalCount Out ,@SlideName , @ParentSlideID ";
            string spname = "dbo.SP_GetSlideMasterList";
            SqlParameter PageSizeParameter = new SqlParameter("@PageSize", PageSize);
            SqlParameter PageIndexParameter = new SqlParameter("@PageIndex", PageIndex);
            var TotalCountParameterOut = new SqlParameter
            {
                ParameterName = "@TotalCount",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Output
            };
            SqlParameter SlideNameParameter = new SqlParameter("@SlideName", string.IsNullOrEmpty(SlideName)?"":SlideName);
            parameters.Add(PageSizeParameter);
            parameters.Add(PageIndexParameter);
            parameters.Add(TotalCountParameterOut);
            parameters.Add(SlideNameParameter);

            try
            {   
                //var data=_context.Database.ExecuteSqlRaw(spname, parameters); // working
                ds = _context.DbDataSet(spname, parameters);
                int TotalCount = Convert.ToInt32(TotalCountParameterOut.Value);
                RD = new DtoReturnData
                {
                    Data = ds.Tables[0],
                    Message = "Success",
                    Status = true,
                    OtherData = "",
                    TotalCount = TotalCount,
                    HttpStatus = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                RD = new DtoReturnData
                {
                    Data = null,
                    Message = ex.Message ,
                    Status = false,
                    OtherData = "",
                    TotalCount = 0,
                    HttpStatus = HttpStatusCode.OK
                };
            }
            return await Task.FromResult(RD);
        }

        [HttpPost]
        public async Task<DtoReturnData> AddSlideMaster([FromBody] SlideMaster slide)
        {
            try
            {
                _context.SlideMasters.Add(slide);
                
                await _context.SaveChangesAsync();
                RD = new DtoReturnData
                {
                    Data = slide,
                    Message = "success",
                    Status = true,
                    OtherData = "",
                    TotalCount = 0,
                    HttpStatus = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                RD = new DtoReturnData
                {
                    Data = "",
                    Message = ex.Message.ToString(),
                    Status = false,
                    OtherData = "",
                    TotalCount = 0,
                    HttpStatus = HttpStatusCode.OK
                };
            }

            return await Task.FromResult(RD);
        }
       
        [HttpPost]
        public async Task<IActionResult> UpdateSlideMaster([FromBody] SlideMaster slide)
        {
            
            _context.Entry(slide).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlideMasterExists(slide.SlideId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<DtoReturnData> GetSlideMaster(int id)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> parameters=new List<SqlParameter>();
            //string procedureName = "dbo.SP_GetSlideMasterList @PageSize, @PageIndex, @TotalCount Out ,@SlideName , @ParentSlideID ";
            string spname = "dbo.SP_GetSlideMaster";
            SqlParameter SlideIDParameter = new SqlParameter("@SlideID", id);
            parameters.Add(SlideIDParameter);

            try
            {   
                //var data=_context.Database.ExecuteSqlRaw(spname, parameters); // working
                ds = _context.DbDataSet(spname, parameters);
                RD = new DtoReturnData
                {
                    Data = ds.Tables[0],
                    Message = "Success",
                    Status = true,
                    OtherData = "",
                    TotalCount = 0,
                    HttpStatus = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                RD = new DtoReturnData
                {
                    Data = null,
                    Message = ex.Message ,
                    Status = false,
                    OtherData = "",
                    TotalCount = 0,
                    HttpStatus = HttpStatusCode.OK
                };
            }
            return await Task.FromResult(RD);
        }

        
        
        [HttpPost]
        //[HttpPost]
        public async Task<DtoReturnData> UploadSlideImage([FromForm] UploadImageParam objfile)
        {
            //LogWriter log = new LogWriter("start create message");
            DtoReturnData RD = null;
            string message="";
            try
            {
                /*
                if (HttpContext.Request.Form.Files.Any())
                {
                    var imagefile = HttpContext.Request.Form.Files["ImageFile"];
                }
                */
                if (objfile.ImageFile != null)
                {
                    if (objfile.ImageFile!.Length > 0)
                    {
                        
                        if (!Directory.Exists(_environment.ContentRootPath + "\\Images"))
                        {
                            Directory.CreateDirectory(_environment.ContentRootPath + "\\Images");
                        }
                        string fName = objfile.ImageFile.FileName;
                        string extention = Path.GetExtension(fName);
                        string filename = objfile.Id.ToString() + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                        filename = filename + extention;
                        //string path = Path.Combine(_environment.ContentRootPath, "UploadFiles/" + objFile.ImageFile.FileName);
                        string path = Path.Combine(_environment.ContentRootPath, "Images/" + filename);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            objfile.ImageFile.CopyTo(stream);
                            stream.Flush();
                        }
                        if (objfile.ImageId==0)
                        {
                            using (var transaction = _context.Database.BeginTransaction())
                            {
                                try
                                {
                                    ImageMaster imageMaster = new ImageMaster()
                                    {
                                        ImageName = filename,
                                        ImageDate = DateTime.Now,
                                        ImageId=0,
                                        ImageTypeId = 1,
                                        IsActive = true,
                                        Extension = extention
                                    };

                                    _context.ImageMasters.Add(imageMaster);
                                    await _context.SaveChangesAsync();
                                    _context.Database.ExecuteSqlRaw("Update SlideMaster set ImageID="+ imageMaster.ImageId +" where SlideID="+ objfile.Id);
                                    await _context.SaveChangesAsync();
                                    await transaction.CommitAsync();
                                }
                                catch (Exception ex)
                                {
                                    await transaction.RollbackAsync();
                                    Console.WriteLine("Error occurred." + ex.Message);
                                    message=ex.Message;
                                }
                            }
                        }
                        else
                        {
                            ImageMaster imageMaster = new ImageMaster()
                            {
                                ImageName = filename,
                                ImageDate = DateTime.Now,
                                ImageId = (long)objfile.ImageId,
                                ImageTypeId = 1,
                                IsActive = true,
                                Extension = extention
                            };
                            _context.Entry(imageMaster).State = EntityState.Modified;

                            try
                            {
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException ex)
                            {
                                message=ex.Message;
                            }
                        }
                        RD = new DtoReturnData
                        {
                            Data = "",
                            Message = message,
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
                            Message = "No File.",
                            Status = false,
                            OtherData = "",
                            TotalCount = 0,
                            HttpStatus = HttpStatusCode.OK
                        };
                    }
                }
                else
                {
                    RD = new DtoReturnData
                    {
                        Data = "",
                        Message = "No file",
                        Status = false,
                        OtherData = "",
                        TotalCount = 0,
                        HttpStatus = HttpStatusCode.OK
                    };
                }
            }
            catch (Exception ex)
            {
                RD = new DtoReturnData
                {
                    Data = "",
                    Message = "Error " + ex.Message,
                    Status = false,
                    OtherData = "",
                    TotalCount = 0,
                    HttpStatus = HttpStatusCode.BadRequest
                };
                //throw;
            }

            return await Task.FromResult(RD);
        }
    
       [HttpPost]
       public async Task<DtoReturnData> UploadSlideImageType([FromForm] UploadImageParamType objfile)
        {
            //LogWriter log = new LogWriter("start create message");
            DtoReturnData RD = null;
            string message="";
            try
            {
                /*
                if (HttpContext.Request.Form.Files.Any())
                {
                    var imagefile = HttpContext.Request.Form.Files["ImageFile"];
                }
                */
                if (objfile.ImageFile != null)
                {
                    if (objfile.ImageFile!.Length > 0)
                    {
                        
                        if (!Directory.Exists(_environment.ContentRootPath + "\\Images"))
                        {
                            Directory.CreateDirectory(_environment.ContentRootPath + "\\Images");
                        }
                        string fName = objfile.ImageFile.FileName;
                        string extention = Path.GetExtension(fName);
                        string filename = objfile.Id.ToString() + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                        filename = filename + extention;
                        //string path = Path.Combine(_environment.ContentRootPath, "UploadFiles/" + objFile.ImageFile.FileName);
                        string path = Path.Combine(_environment.ContentRootPath, "Images/" + filename);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            objfile.ImageFile.CopyTo(stream);
                            stream.Flush();
                        }
                        if (objfile.ImageId==0)
                        {
                            using (var transaction = _context.Database.BeginTransaction())
                            {
                                try
                                {
                                    ImageMaster imageMaster = new ImageMaster()
                                    {
                                        ImageName = filename,
                                        ImageDate = DateTime.Now,
                                        ImageId=0,
                                        ImageTypeId = 1,
                                        IsActive = true,
                                        Extension = extention
                                    };
                                    
                                    _context.ImageMasters.Add(imageMaster);
                                    await _context.SaveChangesAsync();
                                    string query="";
                                    if(objfile.TypeId==1){ 
                                        //Image Small
                                        query="Update SlideMaster set ImageIDSM="+ imageMaster.ImageId +" where SlideID="+ objfile.Id;
                                    }
                                    else{
                                        //Image Large
                                        query="Update SlideMaster set ImageID="+ imageMaster.ImageId +" where SlideID="+ objfile.Id;

                                    }
                                    _context.Database.ExecuteSqlRaw(query);
                                    await _context.SaveChangesAsync();
                                    await transaction.CommitAsync();
                                }
                                catch (Exception ex)
                                {
                                    message=ex.Message;
                                    await transaction.RollbackAsync();
                                    Console.WriteLine("Error occurred." + ex.Message);
                                }
                            }
                        }
                        else
                        {
                            ImageMaster imageMaster = new ImageMaster()
                            {
                                ImageName = filename,
                                ImageDate = DateTime.Now,
                                ImageId = (long)objfile.ImageId,
                                ImageTypeId = 1,
                                IsActive = true,
                                Extension = extention
                            };
                            _context.Entry(imageMaster).State = EntityState.Modified;

                            try
                            {
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException ex)
                            {
                                message=ex.Message;
                            }
                        }
                        RD = new DtoReturnData
                        {
                            Data = "",
                            Message = message,
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
                            Message = "No File.",
                            Status = false,
                            OtherData = "",
                            TotalCount = 0,
                            HttpStatus = HttpStatusCode.OK
                        };
                    }
                }
                else
                {
                    RD = new DtoReturnData
                    {
                        Data = "",
                        Message = "No file",
                        Status = false,
                        OtherData = "",
                        TotalCount = 0,
                        HttpStatus = HttpStatusCode.OK
                    };
                }
            }
            catch (Exception ex)
            {
                RD = new DtoReturnData
                {
                    Data = "",
                    Message = "Error " + ex.Message,
                    Status = false,
                    OtherData = "",
                    TotalCount = 0,
                    HttpStatus = HttpStatusCode.BadRequest
                };
                //throw;
            }

            return await Task.FromResult(RD);
        }

        private bool SlideMasterExists(int id)
        {
            return (_context.SlideMasters?.Any(e => e.SlideId == id)).GetValueOrDefault();
        }

    }
}

