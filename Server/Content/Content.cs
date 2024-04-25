using System.Reflection;

namespace RMUD3.Server.Content
{
	public class Content
	{

		private static Content? instance;

		private readonly Dictionary<string, ContentType> contentTypes;

		private Content()
		{
			contentTypes = [];
		}

		public static async Task Load()
		{
			instance = new();
			var task = instance.LoadInstance();
			await task;

			if (task.IsFaulted)
			{
				Console.WriteLine("Failed to load content");
				Console.WriteLine(task.Exception);
			}
		}

		private void MapContentTypes()
		{
			// Get all classes that inherit from ContentType
			var types = Assembly.GetExecutingAssembly().GetTypes();
			var contentTypes = types.Where(t => t.IsSubclassOf(typeof(ContentType))).ToArray();
			foreach (Type type in contentTypes)
			{
				// Create an instance of each type
				var contentType = (ContentType?)Activator.CreateInstance(type) ?? throw new Exception($"Failed to create instance of ContentType: {type.Name}");

				// Add the instance to the contentTypes dictionary
				this.contentTypes.Add(contentType.TypeName, contentType);
			}
		}

		private async Task LoadInstance()
		{
			Console.WriteLine("Loading content...");

			MapContentTypes();
			FindContentFiles();

			foreach (ContentType contentType in contentTypes.Values)
				contentType.Load();

			Console.WriteLine("Content loaded");
		}

		private void FindContentFiles()
		{
			string[] filePaths = Directory.GetFiles("Content", "*.json", SearchOption.AllDirectories);

			Console.WriteLine($"Found {filePaths.Length} content files");
			foreach (string filePath in filePaths)
			{
				try
				{
					ContentFile file = new(filePath);
					contentTypes.TryGetValue(file.Type, out ContentType? contentType);
					if (contentType == null)
						throw new Exception($"No content type found for file: {file.Name}.{file.Type}");

					contentType.AddFile(file);
				}
				catch (ArgumentException e)
				{
					Console.WriteLine($"Failed to load file: {filePath}");
					Console.WriteLine("\t" + e.Message);
				}
			}
		}

	}
}
