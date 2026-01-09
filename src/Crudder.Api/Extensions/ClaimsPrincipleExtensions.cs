using System.Security.Claims;

namespace Crudder.Api.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException("UserId claim is missing");
            return int.Parse(claim.Value);
        }
    }
}