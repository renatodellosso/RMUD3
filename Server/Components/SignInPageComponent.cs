using Tapper;

namespace RMUD3.Server.Components
{

	[TranspilationSource]
	public enum SignInPageClientAction
	{
		SignIn,
		CreateAccount,
	}

	[TranspilationSource]
	public enum SignInPageServerAction
	{
		SignInError,
		CreateAccountError,
	}

	public class SignInPageComponent : Component
	{
		public SignInPageComponent(Session session) : base("root", session)
		{
			ActionHandler = new()
			{
				{ SignInPageClientAction.SignIn, ActionHandler.Wrap((SignInCredentials creds) =>
					{
						var auth = SignIn(creds);

						if (auth.Account is not null)
						{
							session.Account = auth.Account;
							ExecuteAction(SignInPageServerAction.SignInError, "Signed in successfully");
							return;
						}

						ExecuteAction(SignInPageServerAction.SignInError, auth.ErrorMessage);
					})
				},
				{ SignInPageClientAction.CreateAccount, ActionHandler.Wrap((SignInCredentials creds) =>
					{
						var auth = CreateAccount(creds);

						if (auth.Account is not null)
						{
							session.Account = auth.Account;
							ExecuteAction(SignInPageServerAction.CreateAccountError, "Account created successfully");
							return;
						}

						ExecuteAction(SignInPageServerAction.CreateAccountError, auth.ErrorMessage);
					})
				},
			};
		}

		private record class AuthStatus
		{
			public Account? Account { get; init; }
			public string? ErrorMessage { get; init; }

			public AuthStatus(Account account)
			{
				Account = account;
			}

			public AuthStatus(string errorMessage)
			{
				ErrorMessage = errorMessage;
			}
		}

		private static AuthStatus CreateAccount(SignInCredentials creds)
		{
			var db = Services.Db ?? throw new InvalidOperationException("DbService not initialized");
			var accounts = db.Accounts;

			Console.WriteLine($"Creating account: {creds.Username}");
			if (creds.Username.Length < 4)
				return new("Username must be at least 4 characters");
			if (creds.Username.Length > 20)
				return new("Password must be at most 20 characters");

			if (creds.Password == null)
				return new("Password cannot be null");

			if (accounts.Any(a => a.Username == creds.Username))
				return new("Username taken");

			var account = new Account(creds);
			accounts.Add(account);
			db.SaveChanges();

			Console.WriteLine($"Account created: {creds.Username}");
			return new(account);
		}

		private static AuthStatus SignIn(SignInCredentials creds)
		{
			var db = Services.Db ?? throw new InvalidOperationException("DbService not initialized");
			var accounts = db.Accounts;

			if (creds.Password == null)
				return new("Password cannot be null");

			const string ERROR_MSG = "Incorrect username or password";
			var account = accounts.FirstOrDefault(a => a.Username == creds.Username);
			if (account is null)
				return new(ERROR_MSG);

			Console.WriteLine(creds.Password);
			if (!account.VerifyPassword(creds.Password))
			{
				Console.WriteLine($"User failed to sign in: {creds.Username}");
				return new(ERROR_MSG);
			}

			Console.WriteLine($"Account signed in: {creds.Username}");
			return new(account);
		}

	}
}