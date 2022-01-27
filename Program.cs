using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using HelloMUDWorld.Areas.Identity;
using HelloMUDWorld.Data;
using HelloMUDWorld.Server.Hubs;
using HelloMUDWorld.Data.Identity;

// =========================
//          BUILDER
// -------------------------
//  1. Adding Services
// =========================

var builder = WebApplication.CreateBuilder(args);

// # Database

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<HelloMUDWorldContext>(options =>
    options.UseSqlite(connectionString));

builder.Services
    .AddDatabaseDeveloperPageExceptionFilter();

// # Identity

builder.Services
    .AddDefaultIdentity<HelloMUDWorldUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<HelloMUDWorldContext>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<TokenProvider>();

// # Razor & Blazor

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// # Compression

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

// # Authentication

builder.Services
    .AddScoped<
        AuthenticationStateProvider,
        RevalidatingIdentityAuthenticationStateProvider<IdentityUser>
    >();

// # Work Services

builder.Services.AddSingleton<WeatherForecastService>();

// -------------------------
//  2. Configuring Services
// -------------------------

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

// =====================
//          APP
// =====================

var app = builder.Build();

app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();

// Hubs

app.MapHub<ChatHub>("/chathub");

app.MapFallbackToPage("/_Host");

app.Run();