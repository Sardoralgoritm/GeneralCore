namespace GeneralCore.Authorization.Attributes;

/// <summary>
/// Marks an enum field as a permission sub-group.
/// Use [Translate] attributes for additional language support.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class PermissionSubGroupAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
