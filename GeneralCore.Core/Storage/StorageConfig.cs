namespace GeneralCore.Storage;

public class StorageConfig
{
    public string Endpoint      { get; set; } = string.Empty;
    public string AccessKey     { get; set; } = string.Empty;
    public string SecretKey     { get; set; } = string.Empty;
    public string BucketName    { get; set; } = string.Empty;
    public bool   UseSSL        { get; set; } = false;

    /// <summary>
    /// Bucket hamma uchun ochiq bo'lsinmi. Default — yopiq.
    /// true qilinsa, bucket yaratilayotganda anonim o'qishga ruxsat beruvchi
    /// policy qo'yiladi va fayllarni URL orqali istalgan kishi ochadi.
    /// Maxfiy hujjatlar uchun false qoldiring va DownloadAsync ishlating.
    /// </summary>
    public bool   IsPublic      { get; set; } = false;

    /// <summary>
    /// Fayllarning public URL prefiksi.
    /// Masalan: https://minio.ssardor.uz/ansor-market
    /// Bo'sh qolsa: http(s)://{Endpoint}/{BucketName} ishlatiladi.
    /// </summary>
    public string PublicBaseUrl { get; set; } = string.Empty;
}
