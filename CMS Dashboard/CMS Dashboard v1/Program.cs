using CMS_Dashboard_v1.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.Configuration;
using System.Net.Http.Headers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
  .AddAuthentication(options => {
      options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      options.RequireAuthenticatedSignIn = true;
  })
  .AddCookie(options => {
      options.Cookie.Name = "_Auth";
      options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
      options.LoginPath = "/Login";
      options.LogoutPath = "/Auth/Logout";
  });

builder.Services
  .AddAuthorization(options => {
      options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
  });

builder.Services.AddSession();
builder.Services.AddResponseCaching();



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
app.UseResponseCaching();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=LandingPage}/{controller=Habaku}/{action=Index}/{id?}");

app.Run();
