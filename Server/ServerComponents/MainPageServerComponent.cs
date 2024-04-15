using Tapper;

namespace RMUD3.Server.ServerComponents
{

	[TranspilationSource]
	public enum MainPageClientAction
	{
		MainPage
	}

	public class MainPageServerComponent : ServerComponent
	{
		public MainPageServerComponent(Session session) : base("MainPage", session)
		{
			AddActionHandler(MainPageClientAction.MainPage, (string args) =>
			{
				Console.WriteLine("MainPage action received! Args: " + args);
			});
		}
	}
}