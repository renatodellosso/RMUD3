namespace RMUD3.Server.Content.Lists
{
	public class News
	{

		/// <summary>
		/// How many news items to display in the feed. Will display the most recent items.
		/// </summary>
		private const int FEED_LENGTH = 5;

		private static NewsItem[]? items;

		/// <summary>
		/// Will return the most recent news items. Number of items is configurable through <see cref="FEED_LENGTH"/>.
		/// If there are no news items, will return an empty array.
		/// </summary>
		public static NewsItem[] Items
		{
			get => items?[..Math.Min(FEED_LENGTH, items.Length)] ?? []; set => items = value;
		}

	}
}
