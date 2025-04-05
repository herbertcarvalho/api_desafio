using Backend.Erp.Skeleton.Application.DTOs;
using System.Security.Claims;

namespace Backend.Erp.Skeleton.Application.Extensions
{
    public static class UserExtensions
    {
        public static UserClaim GetUser(this ClaimsPrincipal user)
        {


            var userClaim = new UserClaim();

            return userClaim;
        }
    }
}
