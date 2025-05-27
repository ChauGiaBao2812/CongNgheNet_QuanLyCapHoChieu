using Microsoft.EntityFrameworkCore;
using QuanLiHoChieu.Data;
using QuanLiHoChieu.Services.Interface;
using QuanLiHoChieu.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PassportDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Chung/Login";          // Redirect here if not logged in
        options.AccessDeniedPath = "/Chung/AccessDenied"; // Redirect here if no permission
        options.Cookie.Name = "MyCookieAuth";           // Cookie name in browser
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(1); // Optional: cookie lifetime
        options.SlidingExpiration = false;                // Optional: refresh cookie expiry on activity
        options.Cookie.IsEssential = true;               // GDPR compliance
    });

builder.Services.AddScoped<IGetDataByFormIdService, GetDataByFormIdService>();
builder.Services.AddScoped<IRootAdminService, RootAdminService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Chung}/{action=Home}/{id?}");

app.MapControllerRoute(
    name: "catchall",
    pattern: "{*url}",
    defaults: new { controller = "Chung", action = "NotFoundPage" });

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<PassportDbContext>();
SeedData.SeedDatabase(context);

app.Run();
