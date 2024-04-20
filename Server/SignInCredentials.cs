namespace RMUD3.Server
{
	public class SignInCredentials(string username, string? password)
	{
		public string Username { get; set; } = username;
		public string? Password { get; protected init; } = password;
	}
}
