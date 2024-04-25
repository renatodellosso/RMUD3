namespace RMUD3.Server.Content
{
	public class ContentFile
	{
		public string Path { get; private init; }
		public string Name { get; private init; }
		public string Type { get; private init; }

		public ContentFile(string path)
		{
			Path = path;

			string fullName = path.Split('/', '\\').Last();
			string[] nameSections = fullName.Split('.');

			if (nameSections.Length < 3)
			{
				throw new ArgumentException($"Invalid file name: {fullName}");
			}

			Name = nameSections[0];
			Type = nameSections[^2];
		}

		public string Read()
		{
			return File.ReadAllText(Path);
		}
	}
}
