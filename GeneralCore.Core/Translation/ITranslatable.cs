namespace GeneralCore.Translation;

public interface ITranslatable<TTranslation> where TTranslation : ITranslation
{
    ICollection<TTranslation> Translations { get; set; }
}
