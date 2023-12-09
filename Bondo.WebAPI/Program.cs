using Bondo.Domain.Entities;
using Bondo.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Bondo.Application.Extensions;
using Bondo.Persistence.Extensions;
using Bondo.Infrastructure.Extensions;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// DB context
// builder.Services.AddDbContext<AppSqlDbContext>(options =>
//         options.UseSqlServer(
//             builder.Configuration.GetConnectionString("BondoSqlConn")));

// Add services to the container.
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);

// Auth Config
builder.Services.AddCors(options =>
  options.AddPolicy("Dev", builder =>
  {
    // Allow multiple methods
    builder.WithMethods("GET", "POST", "PATCH", "DELETE", "OPTIONS")
      .WithHeaders(
        HeaderNames.Accept,
        HeaderNames.ContentType,
        HeaderNames.Authorization)
      .AllowCredentials()
      .SetIsOriginAllowed(origin =>
      {
        if (string.IsNullOrWhiteSpace(origin)) return false;
        // For testing with localhost, **remove this line in production!**
        if (origin.ToLower().StartsWith("http://localhost")) return true;
        // production domain.
        if (origin.ToLower().StartsWith("https://bondo.com")) return true;
        return false;
      });
  })
);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
  {
      options.Cookie.Name = "UserLoginCookie";
      options.SlidingExpiration = true;
      options.ExpireTimeSpan = new TimeSpan(1, 0, 0); // Expires in 1 hour
      options.Events.OnRedirectToLogin = (context) =>
      {
          context.Response.StatusCode = StatusCodes.Status401Unauthorized;
          return Task.CompletedTask;
      };
      options.Cookie.HttpOnly = true;
      // Only use this when the sites are on different domains
      options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
  });
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>
    (options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<AppSqlDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(option => {
       option.RoutePrefix = "";
    });
//}

app.UseHttpsRedirection();

app.UseCookiePolicy(
    new CookiePolicyOptions
    {
        Secure = CookieSecurePolicy.Always
    });
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
