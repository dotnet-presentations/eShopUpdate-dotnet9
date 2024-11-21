var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.eShopUpdate_ApiCore>("api");

builder.AddProject<Projects.eShopUpdateCore>("web")
	.WithReference(api)
	.WaitFor(api)
	.WithExternalHttpEndpoints();

builder.Build().Run();
