namespace CarDealership.Infrastructure
{
    using System.Security.Claims;

    using static CarDealership.Areas.Admin.AdminConstants;
    public static class ClaimsPrincipleExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdministratorRoleName);
        }
    }
}
