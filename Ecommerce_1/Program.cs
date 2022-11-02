using Ecommerce_1.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Contexto>
    (options => options.UseMySql(
        "Server=localhost;initial catalog=ECOMMERCE_CRUD;Uid=root;Pwd=1234",
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql")));

builder.Services.AddAuthentication("Identity.Login").AddCookie("Identity.Login", config =>
{
    config.Cookie.Name = "Identity.Login";
    config.LoginPath = "/Login";
    config.AccessDeniedPath = "/Home";
    config.ExpireTimeSpan = TimeSpan.FromHours(1);
});

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
