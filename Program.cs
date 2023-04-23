using BatikanSon.DAL;
using BatikanSon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LangDB>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

//builder.Services.AddDefaultIdentity<Uye>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<LangDB>();

//builder.Services.AddDefaultIdentity<Uye>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<LangDB>();

builder.Services.AddIdentity<Uye, Rol>().AddEntityFrameworkStores<LangDB>();

builder.Services.AddMvc();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "AdminPanel",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "OgretmenPanel",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "OgrenciPanel",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Lang}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
