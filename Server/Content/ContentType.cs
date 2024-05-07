namespace RMUD3.Server.Content
{
	public abstract class ContentType
	{
		public abstract string TypeName { get; }

		private List<ContentFile> files;

		public bool Loaded { get; private set; }

		public virtual Type[] Dependencies => [];

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
			Console.WriteLine($"Loading content type: {TypeName} with {files.Count} files...");
			foreach (ContentFile file in files)
			{
				try { LoadFile(file); }
				catch (ContentLoadException e)
				{
					throw;
				}
				catch (Exception e)
				{
					throw new ContentLoadException(file, e);
				}
			}
			AfterLoad();

			Loaded = true;
		}

		/// <summary>
		/// Called after all files have been loaded.
		/// </summary>
		protected virtual void AfterLoad() { }

		protected abstract void LoadFile(ContentFile file);
	}
}
