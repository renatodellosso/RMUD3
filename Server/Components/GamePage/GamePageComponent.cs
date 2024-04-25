using RMUD3.Server.Gameplay;

namespace RMUD3.Server.Components.GamePage
{
	public class GamePageComponent : Component
	{

		public Session Session { get; private init; }
		public Player Player { get; private init; }

		public GamePageComponent(ComponentManager componentManager) : base("root", componentManager)
		{
			Session = componentManager.Session ?? throw new Exception("Session does not exist, but tried to create GamePageComponent!");
			Player = Session.Player ?? throw new Exception("Player does not exist, but tried to create GamePageComponent!");

			AddChild(new SidebarComponent(this));
		}
	}
}
