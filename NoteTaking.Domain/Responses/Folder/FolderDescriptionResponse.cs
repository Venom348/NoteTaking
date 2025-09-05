namespace NoteTaking.Domain.Responses.Folder;

/// <summary>
///     Класс для представления информации о папки
/// </summary>
public class FolderDescriptionResponse : FolderResponse
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
    
    /// <summary>
    ///     Дата создания папки
    /// </summary>
    public DateTime DateCreated { get; set; }
}