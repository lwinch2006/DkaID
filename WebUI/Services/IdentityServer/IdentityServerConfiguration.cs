using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace WebUI.Services.IdentityServer;

public static class IdentityServerConfiguration
{
	public static List<TestUser> GetTestUsers()
	{
		var testUsers = Enumerable
			.Range(1, 2)
			.Select(t => t switch
			{
				1 => new TestUser
				{
					SubjectId = "{F82B53C8-1B48-4AB7-9C9B-3DE8F7DB4A3C}",
					Username = "dka001@dka.dev",
					Password = "u2u-secret",
					Claims = new List<Claim>
					{
						new Claim("given_name", "D"),
						new Claim(JwtClaimTypes.Name, "D K"),
						new Claim("family_name", "K")
					}
				},
				2 => new TestUser
				{
					SubjectId = "{14A75DCE-8653-4944-9494-D812190B6EF3}",
					Username = "dka002@dka.dev",
					Password = "u2u-secret",
					Claims = new List<Claim>
					{
						new Claim("given_name", "D"),
						new Claim(JwtClaimTypes.Name, "D K"),
						new Claim("family_name", "K")
					}
				}
			});

		return testUsers.ToList();
	}

	public static IEnumerable<IdentityResource> GetIdentityResources()
	{
		var identityResources = Enumerable
			.Range(1, 2)
			.Select<int, IdentityResource>(t => t switch
			{
				1 => new IdentityResources.OpenId(),
				2 => new IdentityResources.Profile()
			});

		return identityResources;
	}

	public static IEnumerable<ApiScope> GetApiScopes()
	{
		var apiScopes = Enumerable
			.Range(1, 2)
			.Select(t => t switch
			{
				1 => new ApiScope("blazor-book-server-app-api.read"),
				2 => new ApiScope("blazor-book-server-app-api.write")
			});

		return apiScopes;
	}

	public static IEnumerable<ApiResource> GetApiResources()
	{
		var apiResources = Enumerable
			.Range(1, 1)
			.Select(t => t switch
			{
				1 => new ApiResource
				{
					Name = "blazor-book-server-app-api",
					DisplayName = "Blazor Book Server App API",
					Scopes =
					{
						"blazor-book-server-app-api.read",
						"blazor-book-server-app-api.write"
					},
				}
			});

		return apiResources;
	}

	public static IEnumerable<Client> GetClients()
	{
		var clients = Enumerable
			.Range(1, 1)
			.Select(t => t switch
			{
				1 => new Client
				{
					ClientName = "Blazor Book Server App",
					ClientId = "blazor-book-server-app",
					AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
					ClientSecrets = { new Secret("u2u-secret".Sha512())},
					AllowedScopes =
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						"blazor-book-server-app-api.read"
					},
					RedirectUris =
					{
						"https://localhost:5556/signin-oidc"
					},
					PostLogoutRedirectUris =
					{
						"https://localhost:5556/signout-callback-oidc"	
					},
					FrontChannelLogoutUri = "https://localhost:5556/signout-oidc",
					RequireConsent = true,
					RequirePkce = false
				}
			});

		return clients;
	}
}