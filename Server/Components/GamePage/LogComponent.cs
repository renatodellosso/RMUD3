using Tapper;

namespace RMUD3.Server.Components.GamePage
{
	[TranspilationSource]
	file enum LogServerAction
	{
		SendMsg
	}

	public class LogComponent : Component
	{
		public LogComponent(GamePageComponent parent) : base("log", parent.ComponentManager!, parent)
		{
		}

		public void SendMsg(string msg)
		{
			ExecuteAction(LogServerAction.SendMsg, msg);
		}
	}
}
