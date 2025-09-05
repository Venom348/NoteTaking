namespace NoteTaking.Domain.Common;

/// <summary>
///     Модель сущности
/// </summary>
public abstract class Entity
{
    /// <summary>
    ///     Идентификатор сущности
    /// </summary>
    public Guid Id { get; set; }
}