using Tapper;

namespace RMUD3.Server.Components
{

	[TranspilationSource]
	public enum MainPageClientAction
	{
		SignIn,
		CreateAccount,
	}

	[TranspilationSource]
	public enum MainPageServerAction
	{

	}

	public class MainPageComponent : Component
	{
		public MainPageComponent(Session session) : base("root", session)
		{
			ActionHandler = new()
			{

			};
		}
	}
}