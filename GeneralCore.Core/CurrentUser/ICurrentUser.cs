namespace GeneralCore.CurrentUser;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    bool IsAdmin { get; }

    string UserName { get; }

    IReadOnlySet<string> Permissions { get; }

    bool HasPermission(string permission);
    bool HasAnyPermission(params string[] permissions);
}
