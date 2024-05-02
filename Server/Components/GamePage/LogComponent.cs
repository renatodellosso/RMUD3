namespace RMUD3.Server.Components.GamePage
{
	public class LogComponent : Component
	{
		public LogComponent(GamePageComponent parent) : base("log", parent.ComponentManager!, parent)
		{
		}
	}
}
