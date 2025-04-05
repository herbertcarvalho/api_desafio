using Backend.Erp.Skeleton.Application.DTOs;
using Backend.Erp.Skeleton.Domain.Enums;
using System.Linq;
using System.Security.Claims;

namespace Backend.Erp.Skeleton.Application.Extensions
{
    public static class UserExtensions
    {
        public static UserClaim GetUser(this ClaimsPrincipal user)
        {
            var userClaim = new UserClaim();

            var userId = user.Claims.FirstOrDefault(x => x.Type == "IdUser");
            if (userId is not null)
                userClaim.IdUser = int.Parse(userId.Value);

            var role = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            if (role is not null)
                userClaim.IdUserType = role.Value.EqualsCase(1.GetEnumDescription<UserTypeEnum>()) ? (int)UserTypeEnum.Company : (int)UserTypeEnum.Client;

            return userClaim;
        }
    }
}
