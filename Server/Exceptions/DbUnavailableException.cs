namespace RMUD3.Server.Exceptions
{
	public class DbUnavailableException : Exception
	{
		public DbUnavailableException() : base("Database service not available.") { }
	}
}
