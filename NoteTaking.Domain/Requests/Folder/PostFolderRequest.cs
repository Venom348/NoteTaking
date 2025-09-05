namespace NoteTaking.Domain.Requests.Folder;

/// <summary>
///     Модель отправки папки на сервер
/// </summary>
public class PostFolderRequest
{
    /// <summary>
    ///     Названия папки
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    ///     Описание папки
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    ///     Идентификатор владельца папки
    /// </summary>
    public Guid User { get; set; }
}