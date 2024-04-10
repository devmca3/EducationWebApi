using System.Text;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using EducationWebApi.Controllers;
using Microsoft.EntityFrameworkCore;
using EducationWebApi.Models;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "allowAll";
/*
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          //builder.WithOrigins("*")
                          builder.AllowAnyOrigin()
                          .SetIsOriginAllowedToAllowWildcardSubdomains()
                         //builder.SetIsOriginAllowed(isOriginAllowed:_=> true)
                         //.SetPreflightMaxAge(TimeSpan.FromSeconds(3600))
                         .AllowAnyHeader()
                         .AllowAnyMethod();
                         //.AllowCredentials();
                      });
});
*/

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
                      {
                          builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                          //.AllowCredentials();
                      });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});
//builder.Services.AddDbContext<db_Context>(options =>
    //options.UseSqlServer(builder.Configuration.GetSection("AppSettings:constr").Value));
    

//builder.Services.AddDbContext<db_Context>(options =>
//    options.UseSqlServer(builder.Configuration.GetSection(@"Data Source=E-5CG22034N0\\SQLEXPRESS;Initial Catalog=db_contacts;User id=sa;password=1234567;Connect Timeout=30;TrustServerCertificate=true;Encrypt=false;").Value));

//builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services
.AddControllersWithViews()
.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
/*
app.UseSwagger();
app.UseSwaggerUI();
*/
//app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();
//app.MapControllers();
app.UseRouting();

//app.UseCors(MyAllowSpecificOrigins);
app.UseCors();
//app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.UseCors(MyAllowSpecificOrigins);
//app.MapControllerRoute( name:"default",pattern: "{controller}/{action}/{id?}");
app.Run();
