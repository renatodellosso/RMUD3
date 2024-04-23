using Tapper;

namespace RMUD3.Server
{
	[TranspilationSource]
	public record NewsItem(string Title, DateTime Date, string Content);
}
