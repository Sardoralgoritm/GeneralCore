using Minio;
using Minio.DataModel.Args;

namespace GeneralCore.Storage;

public class MinioStorageService : IStorageService
{
    private readonly IMinioClient _client;
    private readonly StorageConfig _config;
    private readonly string _publicBaseUrl;

    public MinioStorageService(StorageConfig config)
    {
        _config = config;

        _client = new MinioClient()
            .WithEndpoint(config.Endpoint)
            .WithCredentials(config.AccessKey, config.SecretKey)
            .WithSSL(config.UseSSL)
            .Build();

        _publicBaseUrl = string.IsNullOrWhiteSpace(config.PublicBaseUrl)
            ? $"{(config.UseSSL ? "https" : "http")}://{config.Endpoint}/{config.BucketName}"
            : config.PublicBaseUrl.TrimEnd('/');
    }

    public async Task<string> UploadAsync(
        string folder, string fileName, Stream content, string contentType,
        CancellationToken ct = default)
    {
        var ext = Path.GetExtension(fileName);
        var key = $"{folder.Trim('/')}/{Guid.NewGuid()}{ext}";

        await EnsureBucketAsync(ct);

        await _client.PutObjectAsync(new PutObjectArgs()
            .WithBucket(_config.BucketName)
            .WithObject(key)
            .WithStreamData(content)
            .WithObjectSize(content.Length)
            .WithContentType(contentType),
            ct);

        return key;
    }

    public Task DeleteAsync(string key, CancellationToken ct = default)
        => _client.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(_config.BucketName)
            .WithObject(key),
            ct);

    public string GetUrl(string key)
        => $"{_publicBaseUrl}/{key.TrimStart('/')}";

    private async Task EnsureBucketAsync(CancellationToken ct)
    {
        var exists = await _client.BucketExistsAsync(
            new BucketExistsArgs().WithBucket(_config.BucketName), ct);

        if (!exists)
            await _client.MakeBucketAsync(
                new MakeBucketArgs().WithBucket(_config.BucketName), ct);
    }
}
