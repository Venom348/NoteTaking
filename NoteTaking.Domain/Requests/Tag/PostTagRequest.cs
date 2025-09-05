namespace NoteTaking.Domain.Requests.Tag;

/// <summary>
///     Модель отправки тега на сервер
/// </summary>
public class PostTagRequest
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
}