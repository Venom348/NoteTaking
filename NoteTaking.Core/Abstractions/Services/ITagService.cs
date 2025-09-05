using NoteTaking.Domain.Requests.Tag;
using NoteTaking.Domain.Responses;
using NoteTaking.Domain.Responses.Tag;

namespace NoteTaking.Core.Abstractions.Services;

/// <summary>
///     Сервис для работы с тегами
/// </summary>
public interface ITagService
{
    /// <summary>
    ///     Получение тегов из базы
    /// </summary>
    /// <param name="id">Идентификатор тега</param>
    /// <param name="page">Страница для пагинации</param>
    /// <param name="limit">Лимит пагинации</param>
    /// <returns></returns>
    Task<List<TagDescriptionResponse>> Get(Guid? id, int page = 0, int limit = 20);

    /// <summary>
    ///     Создание тега
    /// </summary>
    /// <param name="request">Данные о создании тега</param>
    /// <returns></returns>
    Task<TagDescriptionResponse> Create(PostTagRequest request);
    
    /// <summary>
    ///     Изменение тега
    /// </summary>
    /// <param name="request">Данные об обновлении тега</param>
    /// <returns></returns>
    Task<TagDescriptionResponse> Update(PatchTagRequest request);
    
    /// <summary>
    ///     Удаление тега
    /// </summary>
    /// <param name="id">Идентификатор тега</param>
    /// <returns></returns>
    Task<TagResponse> Delete(Guid id);
}