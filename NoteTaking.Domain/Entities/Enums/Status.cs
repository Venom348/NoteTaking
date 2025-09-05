namespace NoteTaking.Domain.Entities.Enums;

/// <summary>
///     Статус заметки
/// </summary>
public enum Status
{
    /// <summary>
    ///     Активная заметка
    /// </summary>
    Active,
    
    /// <summary>
    ///     Заметка в архиве
    /// </summary>
    Archived,
    
    /// <summary>
    ///     Удалённая заметка
    /// </summary>
    Deleted
}