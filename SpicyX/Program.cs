using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpicyX.DAL;
using SpicyX.Models;
using SpicyX.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    //    options.Password.RequireNonAlphanumeric = false;
    //    options.Password.RequiredLength = 8;
    //    options.Password.RequireDigit = true;
    //    options.Password.RequireLowercase = true;
    //    options.Password.RequireUppercase = true;

    //    options.User.RequireUniqueEmail = true;

    //    options.Lockout.AllowedForNewUsers = true;
    //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    //    options.Lockout.MaxFailedAccessAttempts = 3;

    //    options.SignIn.RequireConfirmedAccount = true;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(cfg => { cfg.LoginPath = $"/Admin/Account/Login/{cfg.ReturnUrlParameter}"; });

builder.Services.AddScoped<LayoutService>();

var app = builder.Build();


app.UseRouting();
app.UseStaticFiles(); 

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("default","{controller=home}/{action=index}/{id?}");


app.Run();
