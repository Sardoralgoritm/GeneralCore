namespace GeneralCore.Models;

public interface IHaveIdProp<TId>
{
    TId Id { get; set; }
}
