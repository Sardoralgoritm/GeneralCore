using GeneralCore.Constants;

namespace GeneralCore.Results;

/// <summary>
/// User-facing business error with translations.
/// Only known business/validation errors should be AppError.
/// System exceptions must never be exposed — let them propagate to the global exception middleware.
/// </summary>
public sealed record AppError(string Code, string En, string UzLatn, string Ru, string? UzCyrl = null)
{
    public string GetMessage(int langId) => langId switch
    {
        LanguageIdConst.RU      => Ru,
        LanguageIdConst.UZ_LATN => UzLatn,
        LanguageIdConst.UZ_CYRL => UzCyrl ?? UzLatn,
        LanguageIdConst.EN      => En,
        _                       => En
    };
}
