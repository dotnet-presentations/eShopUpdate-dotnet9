
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSystemWebAdapters();
builder.Services.AddHttpForwarder();

builder.ConfigureApiService(builder.Configuration);


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSystemWebAdapters();

app.MapDefaultControllerRoute();
//app.MapForwarder("/{**catch-all}", app.Configuration["ProxyTo"]).Add(static builder => ((RouteEndpointBuilder)builder).Order = int.MaxValue);

app.Run();

public static class WebAppExtensions
{
	public static void ConfigureApiService(this WebApplicationBuilder builder, IConfiguration configuration)
	{
		var baseAddress = configuration["ApiAddress"];
		builder.Services.AddHttpClient("api", client =>
		{
			client.BaseAddress = new Uri(baseAddress);
		});
	}
}
