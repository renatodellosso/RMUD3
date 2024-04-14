using System.Security.Claims;

namespace RMUD3.server
{
	public static class Extensions
	{

		public static string GetSessionId(this ClaimsPrincipal user) => user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;


	}
}
