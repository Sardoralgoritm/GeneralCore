namespace GeneralCore.Authorization.Sync;

public interface ISyncablePermission<TTranslation>
    where TTranslation : ISyncableTranslation
{
    string Code { get; set; }
    string Name { get; set; }
    int StateId { get; set; }
    ICollection<TTranslation> Translations { get; }
}
