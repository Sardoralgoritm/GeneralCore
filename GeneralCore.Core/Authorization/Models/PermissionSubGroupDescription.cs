namespace GeneralCore.Authorization.Models;

public class PermissionSubGroupDescription(string code, string name, List<TranslateDescription> translates)
{
    public string Code { get; } = code;
    public string Name { get; } = name;
    public List<TranslateDescription> Translates { get; } = translates;
    public List<PermissionCodeDescription> Permissions { get; } = [];
}
