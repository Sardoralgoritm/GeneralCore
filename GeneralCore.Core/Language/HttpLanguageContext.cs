using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GeneralCore.Language;

public class HttpLanguageContext(IHttpContextAccessor httpContextAccessor, IOptions<LanguageContextOptions> options)
    : ILanguageContext
{
    private readonly LanguageContextOptions _options = options.Value;

    public int LanguageId
    {
        get
        {
            var header = httpContextAccessor.HttpContext?.Request.Headers[_options.HeaderName].ToString();

            if (!string.IsNullOrEmpty(header) && _options.CodeToId.TryGetValue(header, out var id))
                return id;

            return _options.DefaultLanguageId;
        }
    }
}
