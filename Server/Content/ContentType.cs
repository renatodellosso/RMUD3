namespace RMUD3.Server.Content
{
	public abstract class ContentType
	{
		public abstract string TypeName { get; }

		private List<ContentFile> files;

		public ContentType()
		{
			files = [];
		}

		public void AddFile(ContentFile file)
		{
			files.Add(file);
		}

		public void Load()
		{
			Console.WriteLine($"Loading content type: {TypeName}...");
			foreach (ContentFile file in files)
				LoadFile(file);
		}

		protected abstract void LoadFile(ContentFile load);
	}
}
