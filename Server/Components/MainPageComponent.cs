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
		SignInError,
		CreateAccountError,
	}

	public class MainPageComponent : Component
	{
		public MainPageComponent(Session session) : base("root", session)
		{
			ActionHandler = new()
			{
				{ MainPageClientAction.SignIn, ActionHandler.Wrap(() => ExecuteAction(MainPageServerAction.SignInError, "Test error")) },
				{ MainPageClientAction.CreateAccount, ActionHandler.Wrap(() => ExecuteAction(MainPageServerAction.CreateAccountError, "Test error")) },
			};
		}
	}
}