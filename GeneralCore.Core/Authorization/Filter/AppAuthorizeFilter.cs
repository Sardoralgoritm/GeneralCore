using GeneralCore.CurrentUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GeneralCore.Authorization.Filter;

public class AppAuthorizeFilter(string[] requiredPermissions, ICurrentUser currentUser)
    : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!currentUser.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return Task.CompletedTask;
        }

        if (requiredPermissions.Length > 0 && !currentUser.HasAnyPermission(requiredPermissions))
        {
            context.Result = new ForbidResult();
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}
