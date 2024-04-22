using Tapper;

namespace RMUD3.Server
{
	[TranspilationSource]
	public class NewsItem
	{
		public string Title { get; set; }
		public DateTime Date { get; set; }
		public string Content { get; set; }

		public NewsItem(string title, DateTime date, string content)
		{
			Title = title;
			Date = date;
			Content = content;
		}
	}
}
