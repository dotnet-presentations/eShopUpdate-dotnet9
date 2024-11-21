using Azure.AI.OpenAI;
using Azure.Identity;
using eShopUpdateCore.Services;
using Microsoft.Extensions.AI;
using OpenAI;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

builder.ConfigureApiService(builder.Configuration);

builder.AddAzureOpenAIClient("chatcompletion");
builder.Services.AddChatClient(clientBuilder =>
{
	var client = clientBuilder.Services.GetRequiredService<AzureOpenAIClient>();

    return
		clientBuilder
			.UseOpenTelemetry()
			.UseDistributedCache()
			.Use(client.AsChatClient("gpt-4o-mini"));
});

builder.Services.AddTransient<AISummaryService>();

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
app.UseOutputCache();

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
