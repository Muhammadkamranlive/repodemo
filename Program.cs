using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryCourses;
using RepositoryCourses.Configurations;
using RepositoryCourses.Data_Access;
using RepositoryCourses.Data_Access.Implementation;
using RepositoryCourses.Domain.Repositories;
using RepositoryCourses.Helper;
using RepositoryCourses.Services.Authentication;
using RepositoryCourses.Services.Implementation;
using RepositoryCourses.Services.Interfaces;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<CourseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));
//injecting the jwt
builder.Services.Configure<JwtConfigurations>(builder.Configuration.GetSection("JwtConfigurations"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfigurations:Secret").Value);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, // for dev false other wise will check for ssl
        ValidateAudience = false,  // for dev only,
        RequireExpirationTime = false, // for dev i am putting it false
        ValidateLifetime = true,
    };

});
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

})
 .AddEntityFrameworkStores<CourseContext>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ITeacherSertvice, TeacherService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddSingleton<ICourseService, CourseService>();
builder.Services.AddSingleton<ICoverService, CoverService>();
builder.Services.AddSingleton<ITagService, TagService>();
builder.Services.AddSingleton<INozlesServices,NozleService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    containerBuilder.RegisterModule(new RepoModule()));

builder.Services.AddMvc().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllers();
builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", b => b.AllowAnyOrigin()
   .AllowAnyHeader()
   .AllowAnyMethod()
   );
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(HandleProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// middleware

app.ConfigureErrorHandler();
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
