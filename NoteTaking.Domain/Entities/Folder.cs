using NoteTaking.Domain.Common;

namespace NoteTaking.Domain.Entities;

/// <summary>
///     Модель папки
/// </summary>
public class Folder : Entity
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