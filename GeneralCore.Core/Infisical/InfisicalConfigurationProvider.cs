using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GeneralCore.Infisical;

/// <summary>
/// Infisical Universal Auth orqali login qilib, secretlarni o'qib IConfiguration ga inject qiladi.
/// Infisical UI dagi key nomlar to'g'ridan-to'g'ri IConfiguration key sifatida ishlatiladi.
/// Masalan: "Database__ConnectionString" → Configuration["Database:ConnectionString"]
/// </summary>
public class InfisicalConfigurationProvider : ConfigurationProvider
{
    private readonly string _url;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _projectId;
    private readonly string _environment;

    public InfisicalConfigurationProvider(
        string url, string clientId, string clientSecret,
        string projectId, string environment)
    {
        _url          = url.TrimEnd('/');
        _clientId     = clientId;
        _clientSecret = clientSecret;
        _projectId    = projectId;
        _environment  = environment;
    }

    public override void Load()
        => LoadAsync().GetAwaiter().GetResult();

    private async Task LoadAsync()
    {
        using var http = new HttpClient();

        var token = await GetAccessTokenAsync(http);

        http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var requestUrl = $"{_url}/api/v3/secrets/raw" +
                         $"?workspaceId={_projectId}" +
                         $"&environment={_environment}" +
                         $"&secretPath=/";

        var response = await http.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        var json    = await response.Content.ReadAsStringAsync();
        var root    = JsonDocument.Parse(json).RootElement;
        var secrets = root.GetProperty("secrets");

        var data = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

        foreach (var secret in secrets.EnumerateArray())
        {
            var key   = secret.GetProperty("secretKey").GetString()!;
            var value = secret.GetProperty("secretValue").GetString();

            // "Database__ConnectionString" → "Database:ConnectionString"
            data[key.Replace("__", ":")] = value;
        }

        Data = data;
    }

    private async Task<string> GetAccessTokenAsync(HttpClient http)
    {
        var payload = JsonSerializer.Serialize(new
        {
            clientId     = _clientId,
            clientSecret = _clientSecret
        });

        var response = await http.PostAsync(
            $"{_url}/api/v1/auth/universal-auth/login",
            new StringContent(payload, Encoding.UTF8, "application/json"));

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var root = JsonDocument.Parse(json).RootElement;

        return root.GetProperty("accessToken").GetString()!;
    }
}

public class InfisicalConfigurationSource : IConfigurationSource
{
    private readonly string _url;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _projectId;
    private readonly string _environment;

    public InfisicalConfigurationSource(
        string url, string clientId, string clientSecret,
        string projectId, string environment)
    {
        _url          = url;
        _clientId     = clientId;
        _clientSecret = clientSecret;
        _projectId    = projectId;
        _environment  = environment;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
        => new InfisicalConfigurationProvider(_url, _clientId, _clientSecret, _projectId, _environment);
}

public static class InfisicalConfigurationExtensions
{
    public static IConfigurationBuilder AddInfisical(
        this IConfigurationBuilder builder,
        string url,
        string clientId,
        string clientSecret,
        string projectId,
        string environment)
    {
        builder.Add(new InfisicalConfigurationSource(
            url, clientId, clientSecret, projectId, environment));
        return builder;
    }
}
