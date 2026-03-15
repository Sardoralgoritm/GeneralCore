using GeneralCore.Models;

namespace GeneralCore.CurrentUser;

public interface ICurrentUser<TId> : ICurrentUser, IHaveIdProp<TId>
{
}
