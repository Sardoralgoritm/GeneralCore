namespace GeneralCore.Authorization.Sync;

public interface ISyncablePermissionSubGroup<TTranslation>
    where TTranslation : ISyncableTranslation
{
    string Code { get; set; }
    string Name { get; set; }
    int StateId { get; set; }
    ICollection<TTranslation> Translations { get; }
}
