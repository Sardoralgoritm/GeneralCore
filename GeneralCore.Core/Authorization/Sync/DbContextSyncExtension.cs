using GeneralCore.Authorization.Helpers;
using GeneralCore.Authorization.Attributes;
using GeneralCore.Constants;
using Microsoft.EntityFrameworkCore;

namespace GeneralCore.Authorization.Sync;

public static class DbContextSyncExtension
{
    public static async Task SyncPermissionsAsync<TEnum, TSubGroup, TSubGroupTranslation, TPermission, TPermissionTranslation>(
        this DbContext context,
        Action<TPermission, TSubGroup> linkToSubGroup)
        where TEnum : struct, Enum
        where TSubGroup : class, ISyncablePermissionSubGroup<TSubGroupTranslation>, new()
        where TSubGroupTranslation : class, ISyncableTranslation, new()
        where TPermission : class, ISyncablePermission<TPermissionTranslation>, new()
        where TPermissionTranslation : class, ISyncableTranslation, new()
    {
        var descriptions = ModuleCodeHelper.GetDescriptions<TEnum>();

        var subGroups = await context.Set<TSubGroup>().Include(x => x.Translations).ToListAsync();
        var permissions = await context.Set<TPermission>().Include(x => x.Translations).ToListAsync();

        foreach (var (subGroupCode, subGroupDesc) in descriptions)
        {
            var subGroup = subGroups.FirstOrDefault(x => x.Code == subGroupCode);

            if (subGroup is null)
            {
                subGroup = new TSubGroup
                {
                    Code = subGroupCode,
                    Name = subGroupDesc.Name,
                    StateId = StateIdConst.ACTIVE
                };
                SyncTranslations<TSubGroupTranslation>(subGroup.Translations, subGroupDesc.Translates.Select(t => (t.LanguageId, t.Text)));
                context.Set<TSubGroup>().Add(subGroup);
                subGroups.Add(subGroup);
            }
            else
            {
                subGroup.Name = subGroupDesc.Name;
                subGroup.StateId = StateIdConst.ACTIVE;
                SyncTranslations<TSubGroupTranslation>(subGroup.Translations, subGroupDesc.Translates.Select(t => (t.LanguageId, t.Text)));
            }

            foreach (var permDesc in subGroupDesc.Permissions)
            {
                var permission = permissions.FirstOrDefault(x => x.Code == permDesc.Code);

                if (permission is null)
                {
                    permission = new TPermission
                    {
                        Code = permDesc.Code,
                        Name = permDesc.Name,
                        StateId = StateIdConst.ACTIVE
                    };
                    linkToSubGroup(permission, subGroup);
                    SyncTranslations<TPermissionTranslation>(permission.Translations, permDesc.Translates.Select(t => (t.LanguageId, t.Text)));
                    context.Set<TPermission>().Add(permission);
                    permissions.Add(permission);
                }
                else
                {
                    permission.Name = permDesc.Name;
                    permission.StateId = StateIdConst.ACTIVE;
                    linkToSubGroup(permission, subGroup);
                    SyncTranslations<TPermissionTranslation>(permission.Translations, permDesc.Translates.Select(t => (t.LanguageId, t.Text)));
                }
            }
        }

        // Enum dan olib tashlangan → INACTIVE (delete emas, chunki rollarga biriktirilgan bo'lishi mumkin)
        var activeCodes = descriptions.Values.SelectMany(s => s.Permissions.Select(p => p.Code)).ToHashSet();
        foreach (var permission in permissions.Where(p => !activeCodes.Contains(p.Code)))
            permission.StateId = StateIdConst.INACTIVE;

        var activeSubGroupCodes = descriptions.Keys.ToHashSet();
        foreach (var subGroup in subGroups.Where(s => !activeSubGroupCodes.Contains(s.Code)))
            subGroup.StateId = StateIdConst.INACTIVE;

        await context.SaveChangesAsync();
    }

    private static void SyncTranslations<TTranslation>(
        ICollection<TTranslation> translations,
        IEnumerable<(int LanguageId, string Text)> incoming)
        where TTranslation : class, ISyncableTranslation, new()
    {
        foreach (var (languageId, text) in incoming)
        {
            var existing = translations.FirstOrDefault(t => t.LanguageId == languageId);
            if (existing is null)
                translations.Add(new TTranslation { LanguageId = languageId, Text = text });
            else
                existing.Text = text;
        }
    }
}
