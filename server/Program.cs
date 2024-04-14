using Microsoft.Extensions.FileProviders;

namespace RMUD3.server;

public class Program
{

	private static WebApplication? app;

	public static void Main(string[] args)
	{
		ConfigureApp();

		if (app is null)
		{
			Console.Error.WriteLine("Failed to configure app!");
			Environment.Exit(1);
		}

		Services.Create(app);

		app.Run();
	}

	private static void ConfigureApp()
	{
		var builder = WebApplication.CreateBuilder();

		builder.Services.AddControllers();
		builder.Services.AddSignalR();
		builder.Services.AddSingleton<ISessionManagerService, SessionManagerService>();

		app = builder.Build();

		var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "client", "public"));
		app.UseDefaultFiles(new DefaultFilesOptions()
		{
			FileProvider = fileProvider
		});
		app.UseStaticFiles(new StaticFileOptions()
		{
			FileProvider = fileProvider
		});

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.MapHub<MiddlewareHub>("/hub");
	}

}