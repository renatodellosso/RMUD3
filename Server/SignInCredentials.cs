namespace RMUD3.Server
{
	public class SignInCredentials(string username, string password)
	{
		public string Username { get; private set; } = username;
		public string Password { get; private set; } = password;
	}
}
