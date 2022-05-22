namespace WebUI.Extensions;

public static class WebApplicationExtensions
{
	public static WebApplication ConfigureApp(this WebApplication app)
	{
		if (!app.Environment.IsDevelopment())
		{
			app
				.UseExceptionHandler("/Home/Error")
				.UseHsts();
		}

		app
			.UseHttpsRedirection()
			.UseStaticFiles()
			.UseRouting()
			.UseIdentityServer()
			.UseAuthorization()
			.As<WebApplication>()
			.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

		return app;
	}

	private static T As<T>(this object source)
	{
		return (T)source;
	}
}