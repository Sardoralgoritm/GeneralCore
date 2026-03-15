namespace GeneralCore.Translation;

public static class TranslationExtensions
{
    public static TTranslation? ForLanguage<TTranslation>(
        this IEnumerable<TTranslation> translations,
        int languageId)
        where TTranslation : class, ITranslation
        => translations.FirstOrDefault(t => t.LanguageId == languageId);

    public static TTranslation? ForLanguage<TTranslation>(
        this ICollection<TTranslation>? translations,
        int languageId)
        where TTranslation : class, ITranslation
        => translations?.FirstOrDefault(t => t.LanguageId == languageId);
}
