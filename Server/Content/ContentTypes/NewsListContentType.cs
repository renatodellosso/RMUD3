using System.Text.Json;

namespace RMUD3.Server.Content.ContentTypes
{
	public class NewsListContentType : ContentType
	{
		public override string TypeName => "NewsList";

		protected override void LoadFile(ContentFile file)
		{
			NewsItem[] newsItems = JsonSerializer.Deserialize<NewsItem[]>(file.Read()) ?? throw new Exception($"Failed to read {file.Name} of type: ");
		}
	}
}
