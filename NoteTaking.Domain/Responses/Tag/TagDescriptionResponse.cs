namespace NoteTaking.Domain.Responses.Tag;

/// <summary>
///     Класс для представления игформации о теге
/// </summary>
public class TagDescriptionResponse : TagResponse
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