namespace NoteTaking.Domain.Requests.Note;

/// <summary>
///     Модель изменения заметки на сервере
/// </summary>
public class PatchNoteRequest : PostNoteRequest
{
    /// <summary>
    ///     Идентификатор заметки
    /// </summary>
    public Guid Id { get; set; }
}
