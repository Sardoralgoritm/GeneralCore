namespace GeneralCore.Storage;

public interface IStorageService
{
    /// <summary>
    /// Faylni yuklaydi va uning key ini qaytaradi.
    /// Key formati: {folder}/{guid}.{ext} — masalan: categories/abc123.jpg
    /// </summary>
    Task<string> UploadAsync(string folder, string fileName, Stream content, string contentType, CancellationToken ct = default);

    /// <summary>
    /// Key bo'yicha faylni o'chiradi.
    /// </summary>
    Task DeleteAsync(string key, CancellationToken ct = default);

    /// <summary>
    /// Key bo'yicha faylning public URL ini qaytaradi.
    /// </summary>
    string GetUrl(string key);
}
