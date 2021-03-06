using Microsoft.EntityFrameworkCore;
using Course_API.Data;
using Microsoft.AspNetCore.Identity;
using Course_API.Helpers;
using Course_API.Interfaces;
using Course_API.Repositories;
using Course_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CourseContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequiredLength = 8;

    opt.User.RequireUniqueEmail = true;

    opt.Lockout.MaxFailedAccessAttempts = 5;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(25);
}).AddEntityFrameworkStores<CourseContext>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

// Dependency Injection
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

var corsProfile = "WestcoastEduCors";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(corsProfile,
    policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.WithOrigins("http://localhost:3000");
    });
});

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Students", policy => policy.RequireClaim("Student"));
    opt.AddPolicy("Teachers", policy => policy.RequireClaim("Teacher"));
    opt.AddPolicy("Admins", policy => policy.RequireClaim("Admin"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsProfile);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<CourseContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await SeedDatabase.SeedCategories(context);
    await SeedDatabase.SeedCourses(context);
    await SeedDatabase.SeedUsers(context, userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Ett fel intr??ffade n??r migrering utf??rdes");
}

await app.RunAsync();