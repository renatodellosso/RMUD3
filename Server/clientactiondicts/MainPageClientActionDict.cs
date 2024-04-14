using Tapper;

namespace RMUD3.Server.clientactiondicts
{

	[TranspilationSource]
	public enum MainPageClientAction
	{
		MainPage
	}

	public class MainPageClientActionDict : ClientActionDict
	{
		public MainPageClientActionDict()
		{
			Add(MainPageClientAction.MainPage, (string args) =>
			{
				Console.WriteLine("MainPage action received! Args: " + args);
			});
		}
	}
}