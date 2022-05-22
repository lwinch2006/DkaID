using WebUI.Services.IdentityServer;

namespace WebUI.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services
			.AddIdentityServer()
			.AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
			.AddInMemoryApiScopes(IdentityServerConfiguration.GetApiScopes())
			.AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
			.AddTestUsers(IdentityServerConfiguration.GetTestUsers())
			.AddInMemoryClients(IdentityServerConfiguration.GetClients())
			.AddDeveloperSigningCredential();

		return services;
	}
}