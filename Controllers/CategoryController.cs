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
//using NuGet.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
/*
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Key]
*/

namespace EducationWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CategoryController : Controller
    {
        public static IWebHostEnvironment _environment;
        private readonly db_Context _context;
        DtoReturnData RD = null;
        public CategoryController(IWebHostEnvironment environment,db_Context context)
        {
            _environment = environment;
            _context = context;
        }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<CategoryMaster>>> GetAllCategoryMasters()
        // {

        //     if (_context.CategoryMasters == null)
        //     {
        //         return NotFound();
        //     }
        //     return await _context.CategoryMasters.ToListAsync();
        // }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<CategoryMaster>>> GetParentCategoryMasters()
        // {
        //     if (_context.CategoryMasters == null)
        //     {
        //         return NotFound();
        //     }
        //     return await _context.CategoryMasters.Where(x=> x.ParentCategoryId==null).ToListAsync();
        // }
        [HttpGet]
        //[HttpGet]
        public async Task<DtoReturnData> GetCategoryMasters(int PageSize,int PageIndex,string CategoryName,int ParentCategoryID)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> parameters=new List<SqlParameter>();
            //string procedureName = "dbo.SP_GetCategoryMasterList @PageSize, @PageIndex, @TotalCount Out ,@CategoryName , @ParentCategoryID ";
            string spname = "dbo.SP_GetCategoryMasterList";
            SqlParameter PageSizeParameter = new SqlParameter("@PageSize", PageSize);
            SqlParameter PageIndexParameter = new SqlParameter("@PageIndex", PageIndex);
            var TotalCountParameterOut = new SqlParameter
            {
                ParameterName = "@TotalCount",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Output
            };
            SqlParameter CategoryNameParameter = new SqlParameter("@CategoryName", string.IsNullOrEmpty(CategoryName)?"":CategoryName);
            SqlParameter ParentCategoryIDParameter = new SqlParameter("@ParentCategoryID", ParentCategoryID==0? SqlInt32.Null:ParentCategoryID);

            parameters.Add(PageSizeParameter);
            parameters.Add(PageIndexParameter);
            parameters.Add(TotalCountParameterOut);
            parameters.Add(CategoryNameParameter);
            parameters.Add(ParentCategoryIDParameter);

            try
            {   
                //var data=_context.Database.ExecuteSqlRaw(procedureName, parameters); // working
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
        public async Task<DtoReturnData> AddCategoryMaster([FromBody] CategoryMaster category)
        {
            try
            {
                category.ParentCategoryId = category.ParentCategoryId == 0 ? null : category.ParentCategoryId;
                //_context.CategoryMasters.Add(category);
                
                await _context.SaveChangesAsync();
                RD = new DtoReturnData
                {
                    Data = category,
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

        // [HttpGet("{id}")]
        // public async Task<ActionResult<CategoryMaster>> GetCategoryMaster(int id)
        // {
        //     if (_context.CategoryMasters == null)
        //     {
        //         return NotFound();
        //     }
        //     //var categoryMaster = await _context.CategoryMasters.Where(x => x.CategoryId == id).FirstAsync();

        //     if (categoryMaster == null)
        //     {
        //         return NotFound();
        //     }

        //     return categoryMaster;
        // }

        [HttpGet]
        //[HttpGet]
        public async Task<DtoReturnData> GetCategoryImageMapByCategoryID(int CategoryID)
        {
            DataSet ds = new DataSet();
            string spname = "dbo.SP_GetCategoryImageMapByCategoryID";
            SqlParameter ParentCategoryIDParameter = new SqlParameter("@CategoryID", CategoryID == 0 ? SqlInt32.Null : CategoryID);
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(ParentCategoryIDParameter);
            try
            {
                ds = _context.DbDataSet(spname, parameters);
                RD = new DtoReturnData
                {
                    Data = ds.Tables[0],
                    Message = "Success",
                    Status = true,
                    OtherData = "",
                    TotalCount = ds.Tables[0].Rows.Count,
                    HttpStatus = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                RD = new DtoReturnData
                {
                    Data = null,
                    Message = ex.Message,
                    Status = false,
                    OtherData = "",
                    TotalCount = 0,
                    HttpStatus = HttpStatusCode.OK
                };
            }
            return await Task.FromResult(RD);
        }

        [HttpGet]
        public async Task<DtoReturnData> GetCategoryImageSlideMapByCategoryID(int CategoryID)
        {
            DataSet ds = new DataSet();
            string spname = "dbo.SP_GetCategoryImageSlideMapByCategoryID";
            SqlParameter ParentCategoryIDParameter = new SqlParameter("@CategoryID", CategoryID == 0 ? SqlInt32.Null : CategoryID);
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(ParentCategoryIDParameter);
            try
            {
                ds = _context.DbDataSet(spname, parameters);
                RD = new DtoReturnData
                {
                    Data = ds.Tables,
                    Message = "Success",
                    Status = true,
                    OtherData = "",
                    TotalCount = ds.Tables[0].Rows.Count,
                    HttpStatus = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                RD = new DtoReturnData
                {
                    Data = null,
                    Message = ex.Message,
                    Status = false,
                    OtherData = "",
                    TotalCount = 0,
                    HttpStatus = HttpStatusCode.OK
                };
            }
            return await Task.FromResult(RD);
        }

        [HttpGet]
        public async Task<DtoReturnData> GetCategoryVideoMapByCategoryID(int CategoryID)
        {
            DataSet ds = new DataSet();
            string spname = "dbo.SP_GetCategoryVideoMapByCategoryID";
            SqlParameter ParentCategoryIDParameter = new SqlParameter("@CategoryID", CategoryID == 0 ? SqlInt32.Null : CategoryID);
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(ParentCategoryIDParameter);
            try
            {
                ds = _context.DbDataSet(spname, parameters);
                RD = new DtoReturnData
                {
                    Data = ds.Tables,
                    Message = "Success",
                    Status = true,
                    OtherData = "",
                    TotalCount = ds.Tables[0].Rows.Count,
                    HttpStatus = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                RD = new DtoReturnData
                {
                    Data = null,
                    Message = ex.Message,
                    Status = false,
                    OtherData = "",
                    TotalCount = 0,
                    HttpStatus = HttpStatusCode.OK
                };
            }
            return await Task.FromResult(RD);
        }
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> UpdateCompanyMaster([FromBody] CategoryMaster category)
        {
            
            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryMasterExists(category.CategoryId))
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
        [HttpPost]
        //[HttpPost]
        public async Task<DtoReturnData> UploadCategoryImage([FromForm] CategoryImageParam objfile)
        {
            //LogWriter log = new LogWriter("start create message");
            DtoReturnData RD = null;
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
                        string filename = objfile.CategoryID.ToString() + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                        filename = filename + extention;
                        //string path = Path.Combine(_environment.ContentRootPath, "UploadFiles/" + objFile.ImageFile.FileName);
                        string path = Path.Combine(_environment.ContentRootPath, "Images/" + filename);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            objfile.ImageFile.CopyTo(stream);
                            stream.Flush();
                        }
                        if (objfile.CategoryImageMapId==0)
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
                                    CategoryImageMap categoryImageMap = new CategoryImageMap();
                                    //categoryImageMap.CategoryImageMapId = 0;
                                    categoryImageMap.CategoryId = objfile.CategoryID;
                                    categoryImageMap.ImageId = imageMaster.ImageId;
                                    //_context.CategoryImageMaps.Add(categoryImageMap);
                                    await _context.SaveChangesAsync();
                                    await transaction.CommitAsync();
                                }
                                catch (Exception ex)
                                {
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
                            catch (DbUpdateConcurrencyException)
                            {
                                
                            }
                        }
                        RD = new DtoReturnData
                        {
                            Data = "",
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
        //[HttpPost]
        public async Task<DtoReturnData> UploadCategoryImageSlide([FromForm] CategoryImageSlideParam objfile)
        {
            //LogWriter log = new LogWriter("start create message");
            DtoReturnData RD = null;
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
                        string filename = objfile.CategoryId.ToString() + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                        filename = filename + extention;
                        //string path = Path.Combine(_environment.ContentRootPath, "UploadFiles/" + objFile.ImageFile.FileName);
                        string path = Path.Combine(_environment.ContentRootPath, "Images/" + filename);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            objfile.ImageFile.CopyTo(stream);
                            stream.Flush();
                        }
                        if (objfile.CategoryImgSlideMapId == 0)
                        {
                            using (var transaction = _context.Database.BeginTransaction())
                            {
                                try
                                {
                                    ImageMaster imageMaster = new ImageMaster()
                                    {
                                        ImageName = filename,
                                        ImageDate = DateTime.Now,
                                        ImageId = 0,
                                        ImageTypeId = 2,
                                        IsActive = true,
                                        Extension = extention
                                    };

                                    _context.ImageMasters.Add(imageMaster);
                                    await _context.SaveChangesAsync();
                                    CategoryImageSlideMap categoryImageslideMap = new CategoryImageSlideMap();
                                    //categoryImageMap.CategoryImageMapId = 0;
                                    categoryImageslideMap.CategoryId = objfile.CategoryId;
                                    categoryImageslideMap.ImageId = imageMaster.ImageId;
                                    //_context.CategoryImageSlideMaps.Add(categoryImageslideMap);
                                    await _context.SaveChangesAsync();
                                    await transaction.CommitAsync();
                                }
                                catch (Exception ex)
                                {
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
                                ImageTypeId = 2,
                                IsActive = true,
                                Extension = extention
                            };
                            _context.Entry(imageMaster).State = EntityState.Modified;

                            try
                            {
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }
                        RD = new DtoReturnData
                        {
                            Data = "",
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
        public async Task<DtoReturnData> AddCategoryVideo([FromBody] CategoryVideoParam categoryVideoParam)
        {
            try
            {
                if (categoryVideoParam.CategoryVideoMapId == 0)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            VideoMaster videoMaster = new VideoMaster()
                            {
                                VideoUrl = categoryVideoParam.VideoUrl,
                                IsActive = true
                            };
                            _context.VideoMasters.Add(videoMaster);
                            await _context.SaveChangesAsync();
                            CategoryVideoMap categoryVideoMap = new CategoryVideoMap();
                            categoryVideoMap.CategoryId = categoryVideoParam.CategoryId;
                            categoryVideoMap.VideoId = videoMaster.VideoId;
                            //_context.CategoryVideoMaps.Add(categoryVideoMap);
                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync();
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            Console.WriteLine("Error occurred." + ex.Message);
                        }
                    }
                }
                else
                {
                    VideoMaster videoMaster = new VideoMaster()
                    {
                        VideoUrl = categoryVideoParam.VideoUrl,
                        VideoId = (long)categoryVideoParam.VideoId,
                        IsActive = true
                    };
                    _context.Entry(videoMaster).State = EntityState.Modified;
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                    }
                }
                RD = new DtoReturnData
                {
                    Data = "",
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

        private bool CategoryMasterExists(int id)
        {
            return false;
            //return (_context.CategoryMasters?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }

    }
}

