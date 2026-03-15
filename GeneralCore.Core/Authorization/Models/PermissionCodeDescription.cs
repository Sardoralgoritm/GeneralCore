namespace GeneralCore.Authorization.Models;

public class PermissionCodeDescription(string code, string name, string subGroupCode, List<TranslateDescription> translates)
{
    public string Code { get; } = code;
    public string Name { get; } = name;
    public string SubGroupCode { get; } = subGroupCode;
    public List<TranslateDescription> Translates { get; } = translates;
}
