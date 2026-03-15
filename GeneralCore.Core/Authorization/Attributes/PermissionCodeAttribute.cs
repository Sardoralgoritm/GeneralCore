namespace GeneralCore.Authorization.Attributes;

/// <summary>
/// Marks an enum field as a permission code belonging to a sub-group.
/// Use [Translate] attributes for additional language support.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class PermissionCodeAttribute(object subGroup, string name) : Attribute
{
    public object SubGroup { get; } = subGroup;
    public string Name { get; } = name;
}
