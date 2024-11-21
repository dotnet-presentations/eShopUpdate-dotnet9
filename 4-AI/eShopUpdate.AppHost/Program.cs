var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache")
	.WithRedisInsight();

var api = builder.AddProject<Projects.eShopUpdate_ApiCore>("api");

var openai = builder.AddConnectionString("chatcompletion");

builder.AddProject<Projects.eShopUpdateCore>("web")
	.WithReference(api)
	.WaitFor(api)
	.WithReference(cache)
	.WaitFor(cache)
	.WithReference(openai)
	.WithExternalHttpEndpoints();

builder.Build().Run();
