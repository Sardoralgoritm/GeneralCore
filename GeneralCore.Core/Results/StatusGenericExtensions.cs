using GeneralCore.Language;
using StatusGeneric;

namespace GeneralCore.Results;

/// <summary>
/// Extends StatusGenericHandler to accept AppError (translatable) instead of plain strings.
/// Message is automatically resolved based on the current request language.
/// </summary>
public static class StatusGenericExtensions
{
    /// <summary>
    /// Adds a translated error. Returns the handler for fluent early-return:
    ///   if (notFound) return status.AddError(AppErrors.X.NotFound);
    /// </summary>
    public static StatusGenericHandler AddError(this StatusGenericHandler handler, AppError error)
    {
        handler.AddError(error.GetMessage(LanguageContext.CurrentId), error.Code);
        return handler;
    }

    /// <summary>
    /// Adds a translated error on a typed handler. Returns the handler for fluent early-return.
    /// </summary>
    public static StatusGenericHandler<T> AddError<T>(this StatusGenericHandler<T> handler, AppError error)
    {
        handler.AddError(error.GetMessage(LanguageContext.CurrentId), error.Code);
        return handler;
    }
}
