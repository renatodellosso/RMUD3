using System.Text.Json;

namespace RMUD3.Server
{

	public interface IComponentManager
	{

		ISession? Session { get; set; }

		/// <summary>
		/// Handle a client action.
		/// </summary>
		/// <param name="componentPath">A list of child component IDs. A value of [] executes an action on <see cref="page"/></param>
		/// <param name="actionId">The numerical ID of the action to execute. Don't enter a number! Use a transpiled enum.</param>
		/// <param name="args">JSON to parse into arguments for the action</param>
		/// <returns></returns>
		Task HandleClientAction(string[] componentPath, int action, JsonElement args);
	}

	public class ComponentManager : IComponentManager
	{

		private ISession? session;
		public ISession? Session
		{
			get => session;
			set
			{
				if (session is not null)
					throw new InvalidOperationException("Session already set");
				session = value;
			}
		}

		public Task HandleClientAction(string[] componentPath, int action, JsonElement args)
		{
			throw new NotImplementedException();
		}
	}
}
