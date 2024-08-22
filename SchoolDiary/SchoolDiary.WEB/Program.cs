using Microsoft.EntityFrameworkCore;
using SchoolDiary.BLL.IServices;
using SchoolDiary.BLL.MappingProfile;
using SchoolDiary.BLL.Services;
using SchoolDiary.DAL.DataContext;
using SchoolDiary.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<BusinessLogicLayerMappingProfile>();
});
builder.Services.AddDbContext<SchoolDiaryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDiary"), optionsBuilder =>
    {
        optionsBuilder.MigrationsAssembly("SchoolDiary.DAL");
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IStudentService), typeof(StudentService));
builder.Services.AddScoped(typeof(StudentRepository), typeof(StudentRepository));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
