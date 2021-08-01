namespace ExpenseManager_v2._0.Infrastructure
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
