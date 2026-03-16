using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GeneralCore.Language;

internal class LanguageContextStartupFilter(IHttpContextAccessor accessor, LanguageContextOptions options)
    : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        LanguageContext.Initialize(accessor, options);
        return next;
    }
}
