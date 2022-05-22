namespace WebUI.Extensions;

public static class WebApplicationBuilderExtensions
{
	public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
	{
		builder.Services
			.AddApplication()
			.AddControllersWithViews();
		
		return builder;
	}

	public static WebApplication CreateApp(this WebApplicationBuilder builder)
	{
		return builder.Build();
	}
}