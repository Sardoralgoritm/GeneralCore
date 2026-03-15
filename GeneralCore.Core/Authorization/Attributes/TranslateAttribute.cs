namespace GeneralCore.Authorization.Attributes;

/// <summary>
/// Adds a translation for a specific language to a permission or sub-group.
/// Multiple attributes can be applied for different languages.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class TranslateAttribute(int languageId, string text) : Attribute
{
    public int LanguageId { get; } = languageId;
    public string Text { get; } = text;
}
