namespace GeneralCore.Authorization.Sync;

public interface ISyncableTranslation
{
    int LanguageId { get; set; }
    string Text { get; set; }
}
