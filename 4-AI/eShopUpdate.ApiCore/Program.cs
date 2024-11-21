
using eShopUpdate.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddDbContext<ProductDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Products") ?? throw new InvalidOperationException("Connection string 'Products' not found."));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapDefaultEndpoints();

if (!app.Environment.IsDevelopment())
{
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.CreateDbIfNotExists();

app.UseRouting();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
