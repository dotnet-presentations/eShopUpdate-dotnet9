var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache")
	.WithRedisInsight();

var api = builder.AddProject<Projects.eShopUpdate_ApiCore>("api");

builder.AddProject<Projects.eShopUpdateCore>("web")
	.WithReference(api)
	.WaitFor(api)
	.WithReference(cache)
	.WaitFor(cache)
	.WithExternalHttpEndpoints();

builder.Build().Run();
