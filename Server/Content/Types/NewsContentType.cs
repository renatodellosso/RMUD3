using RMUD3.Server.Content.Lists;
using System.Text.Json;

namespace RMUD3.Server.Content.Types
{
	public class NewsContentType : ContentType
	{
		public override string TypeName => "News";

		private readonly List<NewsItem> newsItems;

		public NewsContentType()
		{
			newsItems = [];
		}

		protected override void LoadFile(ContentFile file)
		{
			var newsItem = JsonSerializer.Deserialize<NewsItem>(file.Read());
			newsItems.Add(newsItem ?? throw new ContentLoadException(file, "Failed to parse JSON"));
		}

		protected override void AfterLoad()
		{
			newsItems.Sort((a, b) => -a.Date.CompareTo(b.Date));
			News.Items = [.. newsItems];
		}
	}
}
