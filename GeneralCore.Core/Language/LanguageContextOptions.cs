using GeneralCore.Constants;

namespace GeneralCore.Language;

public class LanguageContextOptions
{
    public string HeaderName { get; set; } = "Lang";
    public int DefaultLanguageId { get; set; } = LanguageIdConst.UZ_LATN;
    public Dictionary<string, int> CodeToId { get; set; } = new(StringComparer.OrdinalIgnoreCase)
    {
        ["ru"]      = LanguageIdConst.RU,
        ["uz-cyrl"] = LanguageIdConst.UZ_CYRL,
        ["uz-latn"] = LanguageIdConst.UZ_LATN,
        ["en"]      = LanguageIdConst.EN,
        ["qr"]      = LanguageIdConst.QR,
    };
}
