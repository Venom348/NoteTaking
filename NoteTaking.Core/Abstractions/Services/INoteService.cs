using NoteTaking.Domain.Requests.Note;
using NoteTaking.Domain.Responses;
using NoteTaking.Domain.Responses.Note;

namespace NoteTaking.Core.Abstractions.Services;

/// <summary>
///     Сервис для работы с заметками
/// </summary>
public interface INoteService
{
    /// <summary>
    ///     Получение заметок из базы
    /// </summary>
    /// <param name="id">Идентификатор заметки</param>
    /// <param name="page">Страница для пагинации</param>
    /// <param name="limit">Лимит пагинации</param>
    /// <returns></returns>
    Task<List<NoteDescriptionResponse>> Get(Guid? id, int page = 0, int limit = 20);

    /// <summary>
    ///     Создание заметки
    /// </summary>
    /// <param name="request">Данные о создании заметки</param>
    /// <returns></returns>
    Task<NoteDescriptionResponse> Create(PostNoteRequest request);
    
    /// <summary>
    ///     Изменение заметки
    /// </summary>
    /// <param name="request">Данные об обновлении заметки</param>
    /// <returns></returns>
    Task<NoteDescriptionResponse> Update(PatchNoteRequest request);
    
    /// <summary>
    ///     Удаление заметки
    /// </summary>
    /// <param name="id">Идентификатор заметки</param>
    /// <returns></returns>
    Task<NoteResponse> Delete(Guid id);
}