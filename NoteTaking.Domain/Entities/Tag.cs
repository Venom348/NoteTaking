using NoteTaking.Domain.Common;

namespace NoteTaking.Domain.Entities;

/// <summary>
///     Модель тегов
/// </summary>
public class Tag : Entity
{
    /// <summary>
    ///     Название тега
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    ///     Цвет тега
    /// </summary>
    public string Color { get; set; }
    
    /// <summary>
    ///     Идентификатор владельца тега
    /// </summary>
    public Guid User { get; set; }
    
    /// <summary>
    ///     Дата создания тега
    /// </summary>
    public DateTime DateCreated { get; set; }
}