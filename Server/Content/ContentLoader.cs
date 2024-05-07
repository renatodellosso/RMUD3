using System.Reflection;

namespace RMUD3.Server.Content
{
	public class ContentLoader
	{

		private static ContentLoader? instance;

		private readonly Dictionary<string, ContentType> contentTypesByFileExtension;
		private readonly Dictionary<Type, ContentType> contentTypesByType;

		private ContentLoader()
		{
			contentTypesByFileExtension = [];
			contentTypesByType = [];
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
				contentTypesByFileExtension.Add(contentType.TypeName, contentType);
				contentTypesByType.Add(type, contentType);
			}
		}

		private async Task LoadInstance()
		{
			MapContentTypes();
			FindContentFiles();

			LinkedList<ContentType> contentTypes = new();

			// Order content types by dependencies
			foreach (ContentType contentType in contentTypesByFileExtension.Values)
				contentTypes.AddLast(contentType);

			// For each content type, move it after its dependencies
			Console.WriteLine("Ordering content types by dependencies...");
			foreach (ContentType contentType in contentTypesByFileExtension.Values)
			{
				var dependencies = contentType.Dependencies.ToList();

				for (LinkedListNode<ContentType>? node = contentTypes.First; node != null; node = node.Next)
				{
					if (node.Value == contentType)
						continue;

					if (dependencies.Contains(node.Value.GetType()))
						dependencies.Remove(node.Value.GetType());

					if (dependencies.Count == 0)
					{
						contentTypes.Remove(contentType);
						if (contentType.Dependencies.Length == 0)
							contentTypes.AddFirst(contentType);
						else
							contentTypes.AddAfter(node, contentType);
						break;
					}
				}
			}

			Console.WriteLine("Loading content...");
			foreach (ContentType contentType in contentTypes)
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
					contentTypesByFileExtension.TryGetValue(file.Type, out ContentType? contentType);
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
