namespace GeneralCore.Authorization.Attributes;

/// <summary>
/// Marks an enum as a permission code source.
/// </summary>
[AttributeUsage(AttributeTargets.Enum)]
public class PermissionEnumAttribute : Attribute;
