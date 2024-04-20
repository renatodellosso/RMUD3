using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.FileProviders;
using RMUD3.Server.SignalR;

namespace RMUD3.Server;

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

		AddServices(builder);

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

	private static void AddServices(WebApplicationBuilder builder)
	{
		builder.Services.AddControllers();
		builder.Services.AddSignalR();
		builder.Services.AddSingleton<IEnvService, EnvService>();
		builder.Services.AddSingleton<IDbService, DbService>();
		builder.Services.AddSingleton<ISessionManagerService, SessionManagerService>();
		builder.Services.AddSingleton<IUserIdProvider, SessionIdBasedUserIdProvider>();
	}

}