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
						new Claim("family_name", "K"),
						new Claim("address", "Oslo, Norway"),
						new Claim("role", "administrator")
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
						new Claim("family_name", "K"),
						new Claim("address", "Stavanger, Norway"),
						new Claim("role", "user")
					}
				}
			});

		return testUsers.ToList();
	}

	public static IEnumerable<IdentityResource> GetIdentityResources()
	{
		var identityResources = Enumerable
			.Range(1, 4)
			.Select<int, IdentityResource>(t => t switch
			{
				1 => new IdentityResources.OpenId(),
				2 => new IdentityResources.Profile(),
				3 => new IdentityResources.Address(),
				4 => new IdentityResource(name: "roles", displayName: "Roles", userClaims: new []{ "role" })
			});

		return identityResources;
	}

	public static IEnumerable<ApiScope> GetApiScopes()
	{
		var apiScopes = Enumerable
			.Range(1, 2)
			.Select(t => t switch
			{
				1 => new ApiScope("blazor-book-hosted-app-api.read"),
				2 => new ApiScope("blazor-book-hosted-app-api.write")
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
					Name = "blazor-book-hosted-app-api",
					DisplayName = "Blazor Book Hosted App API",
					Scopes =
					{
						"blazor-book-hosted-app-api.read",
						"blazor-book-hosted-app-api.write"
					},
				}
			});

		return apiResources;
	}

	public static IEnumerable<Client> GetClients()
	{
		var clients = Enumerable
			.Range(1, 2)
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
						IdentityServerConstants.StandardScopes.Address,
						"roles",
						"blazor-book-hosted-app-api.read"
					},
					RedirectUris =
					{
						"https://localhost:5000/signin-oidc"
					},
					PostLogoutRedirectUris =
					{
						"https://localhost:5000/signout-callback-oidc"	
					},
					FrontChannelLogoutUri = "https://localhost:5000/signout-oidc",
					RequireConsent = true,
					RequirePkce = false
				},
				2 => new Client
				{
					ClientName = "Blazor Book WASM App",
					ClientId = "blazor-book-wasm-app",
					AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
					ClientSecrets = { new Secret("u2u-secret".Sha512())},
					AllowedScopes =
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Address,
						"roles",
						"blazor-book-hosted-app-api.read"
					},
					RedirectUris =
					{
						"https://localhost:5001/authentication/login-callback"
					},
					PostLogoutRedirectUris =
					{
						"https://localhost:5001/authentication/logout-callback"	
					},
					AllowedCorsOrigins =
					{
						"https://localhost:5001"	
					},
					RequireConsent = false,
					RequirePkce = true,
					RequireClientSecret = false
				}
			});

		return clients;
	}
}