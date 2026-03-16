using Microsoft.AspNetCore.Http;

namespace GeneralCore.Language;

/// <summary>
/// Static accessor — PerDtoConfig lambdalari va boshqa singleton kontekstlarda ishlatish uchun.
/// AddLanguageContext() chaqirilganda avtomatik initializatsiya bo'ladi.
/// </summary>
public static class LanguageContext
{
    private static IHttpContextAccessor? _accessor;
    private static LanguageContextOptions _options = new();

    internal static void Initialize(IHttpContextAccessor accessor, LanguageContextOptions options)
    {
        _accessor = accessor;
        _options = options;
    }

    public static int CurrentId
    {
        get
        {
            var header = _accessor?.HttpContext?.Request.Headers[_options.HeaderName].ToString();

            if (!string.IsNullOrEmpty(header) && _options.CodeToId.TryGetValue(header, out var id))
                return id;

            return _options.DefaultLanguageId;
        }
    }
}
