namespace GeneralCore.Authorization.Models;

public class TranslateDescription(int languageId, string text)
{
    public int LanguageId { get; } = languageId;
    public string Text { get; } = text;
}
