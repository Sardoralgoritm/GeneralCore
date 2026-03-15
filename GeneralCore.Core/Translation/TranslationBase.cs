namespace GeneralCore.Translation;

public abstract class TranslationBase<TOwnerId> : ITranslation
{
    public int Id { get; set; }
    public TOwnerId OwnerId { get; set; } = default!;
    public int LanguageId { get; set; }
}
