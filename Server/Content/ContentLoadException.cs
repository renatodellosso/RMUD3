namespace RMUD3.Server.Content
{
	public class ContentLoadException : Exception
	{
		public ContentLoadException(string message) : base(message) { }

		public ContentLoadException(string message, Exception innerException) : base(message, innerException) { }

		public ContentLoadException(ContentFile file, string message = "")
			: base($"Failed to load file {file.Name} of type {file.Type} (Path: {file.Path}){(message != "" ? $": {message}" : "")}") { }
	}
}
