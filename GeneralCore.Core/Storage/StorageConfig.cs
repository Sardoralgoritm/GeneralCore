namespace GeneralCore.Storage;

public class StorageConfig
{
    public string Endpoint      { get; set; } = string.Empty;
    public string AccessKey     { get; set; } = string.Empty;
    public string SecretKey     { get; set; } = string.Empty;
    public string BucketName    { get; set; } = string.Empty;
    public bool   UseSSL        { get; set; } = false;

    /// <summary>
    /// Fayllarning public URL prefiksi.
    /// Masalan: https://minio.ssardor.uz/ansor-market
    /// Bo'sh qolsa: http(s)://{Endpoint}/{BucketName} ishlatiladi.
    /// </summary>
    public string PublicBaseUrl { get; set; } = string.Empty;
}
