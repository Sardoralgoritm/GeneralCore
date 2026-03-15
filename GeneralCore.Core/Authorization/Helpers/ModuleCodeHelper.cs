using System.Reflection;
using GeneralCore.Authorization.Attributes;
using GeneralCore.Authorization.Models;

namespace GeneralCore.Authorization.Helpers;

public static class ModuleCodeHelper
{
    /// <summary>
    /// Reads TEnum fields via reflection and returns sub-groups with their permissions.
    /// TEnum must be decorated with [PermissionEnum].
    /// </summary>
    public static Dictionary<string, PermissionSubGroupDescription> GetDescriptions<TEnum>()
        where TEnum : struct, Enum
    {
        var enumType = typeof(TEnum);

        if (enumType.GetCustomAttribute<PermissionEnumAttribute>() is null)
            throw new InvalidOperationException(
                $"{enumType.Name} must be decorated with [PermissionEnum] attribute.");

        var subGroups = new Dictionary<string, PermissionSubGroupDescription>();

        foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var permAttr = field.GetCustomAttribute<PermissionCodeAttribute>();
            if (permAttr is null) continue;

            var subGroupCode = permAttr.SubGroup.ToString()!;

            if (!subGroups.TryGetValue(subGroupCode, out var subGroupDesc))
            {
                subGroupDesc = BuildSubGroupDescription(permAttr.SubGroup);
                subGroups[subGroupCode] = subGroupDesc;
            }

            var translates = field
                .GetCustomAttributes<TranslateAttribute>()
                .Select(t => new TranslateDescription(t.LanguageId, t.Text))
                .ToList();

            subGroupDesc.Permissions.Add(new PermissionCodeDescription(
                code: field.Name,
                name: permAttr.Name,
                subGroupCode: subGroupCode,
                translates: translates));
        }

        return subGroups;
    }

    private static PermissionSubGroupDescription BuildSubGroupDescription(object subGroupValue)
    {
        var subGroupType = subGroupValue.GetType();
        var subGroupField = subGroupType.GetField(subGroupValue.ToString()!);

        var subGroupAttr = subGroupField?.GetCustomAttribute<PermissionSubGroupAttribute>()
            ?? throw new InvalidOperationException(
                $"Enum field '{subGroupValue}' must be decorated with [PermissionSubGroup] attribute.");

        var translates = subGroupField!
            .GetCustomAttributes<TranslateAttribute>()
            .Select(t => new TranslateDescription(t.LanguageId, t.Text))
            .ToList();

        return new PermissionSubGroupDescription(
            code: subGroupValue.ToString()!,
            name: subGroupAttr.Name,
            translates: translates);
    }
}
