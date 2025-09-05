namespace NoteTaking.Domain.Requests.Tag;

/// <summary>
///     Модель изменения тега на сервере
/// </summary>
public class PatchTagRequest : PostTagRequest
{
    /// <summary>
    ///     Идентификатор тега
    /// </summary>
    public Guid Id { get; set; }
}