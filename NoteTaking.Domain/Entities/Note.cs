using NoteTaking.Domain.Common;
using NoteTaking.Domain.Entities.Enums;

namespace NoteTaking.Domain.Entities;

/// <summary>
///     Модель заметки
/// </summary>
public class Note : Entity
{
    /// <summary>
    ///     Название заметки
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Содержания заметки
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Идентификатор владельца заметки
    /// </summary>
    public Guid User { get; set; }

    /// <summary>
    ///     Идентификатор папки заметки
    /// </summary>
    public Guid Folder { get; set; }

    /// <summary>
    ///     Список тегов (связь многие ко многим)
    /// </summary>
    public List<Tag> Tags { get; set; }

    /// <summary>
    ///     Статус заметки
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    ///     Признак "Избранная"
    /// </summary>
    public bool IsFavorites { get; set; }
    
    /// <summary>
    ///     Дата создания заметки
    /// </summary>
    public DateTime DateCreated { get; set; }
    
    /// <summary>
    ///     Дата изменения заметки
    /// </summary>
    public DateTime DateModified { get; set; }
    
    /// <summary>
    ///     Признак публичности
    /// </summary>
    public bool IsPublic { get; set; }
}