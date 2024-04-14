using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder();

builder.Services.AddControllers();
builder.Services.AddSignalR();

var app = builder.Build();

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

app.MapHub<TestHub>("/testhub");

app.Run();
