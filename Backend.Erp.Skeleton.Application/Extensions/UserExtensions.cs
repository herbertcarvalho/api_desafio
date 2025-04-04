using System.Linq;
using System.Security.Claims;

namespace Backend.Erp.Skeleton.Application.Extensions
{
    public static class UserExtensions
    {
        public static int GetIdUser(this ClaimsPrincipal user)
        {
            var userId = int.Parse(user.Claims.FirstOrDefault(x => x.Type == "IdUser")?.Value);
            return userId;
        }
    }
}
