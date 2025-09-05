namespace NoteTaking.Domain.Requests.Folder;

/// <summary>
///     Модель изменения папки на сервере
/// </summary>
public class PatchFolderRequest : PostFolderRequest
{
    /// <summary>
    ///     Идентификатор папки
    /// </summary>
    public Guid Id { get; set; }
}