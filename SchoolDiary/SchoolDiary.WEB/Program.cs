using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SchoolDiary.BLL.IServices;
using SchoolDiary.BLL.MappingProfile;
using SchoolDiary.BLL.Services;
using SchoolDiary.DAL.DataContext;
using SchoolDiary.DAL.Repositories;
using SchoolDiary.WEB.MappingProfile;
using SchoolDiary.WEB.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<WebLayerMappingProfile>();
    config.AddProfile<BusinessLogicLayerMappingProfile>();
});
builder.Services.AddDbContext<SchoolDiaryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDiary"), optionsBuilder =>
    {
        optionsBuilder.MigrationsAssembly("SchoolDiary.DAL");
    });
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<SchoolDiaryContext>().AddDefaultTokenProviders();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowClientOrigin", policy =>
    {
        policy.WithOrigins(builder.Configuration.GetSection("ClientOrigin").Value).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
builder.Services.AddScoped(typeof(IStudentService), typeof(StudentService));
builder.Services.AddScoped(typeof(StudentRepository), typeof(StudentRepository));
builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
builder.Services.AddScoped(typeof(IEmailNotificationService), typeof(EmailNotificationService));
builder.Services.AddScoped(typeof(IRoleService), typeof(RoleService));

var app = builder.Build();
app.UseCors("AllowClientOrigin");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
