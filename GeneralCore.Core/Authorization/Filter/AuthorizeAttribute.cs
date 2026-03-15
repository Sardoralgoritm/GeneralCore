using Microsoft.AspNetCore.Mvc;

namespace GeneralCore.Authorization.Filter;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeAttribute : TypeFilterAttribute
{
    // Faqat autentifikatsiya tekshiriladi, permission kerak emas
    public AuthorizeAttribute()
        : base(typeof(AppAuthorizeFilter))
    {
        Arguments = [Array.Empty<string>()];
    }

    // Berilgan permissionlardan kamida bittasi bo'lishi kerak
    public AuthorizeAttribute(params string[] requiredPermissions)
        : base(typeof(AppAuthorizeFilter))
    {
        Arguments = [requiredPermissions];
    }
}
