
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.ConfigureApiService(builder.Configuration);


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

app.UseRouting();
app.UseAuthorization();

app.MapDefaultControllerRoute();
//app.MapForwarder("/{**catch-all}", app.Configuration["ProxyTo"]).Add(static builder => ((RouteEndpointBuilder)builder).Order = int.MaxValue);

app.Run();

public static class WebAppExtensions
{
	public static void ConfigureApiService(this WebApplicationBuilder builder, IConfiguration configuration)
	{
		//var baseAddress = configuration["ApiAddress"];
		builder.Services.AddHttpClient("api", client =>
		{
			client.BaseAddress = new Uri("https+http://api");
		});
	}
}
