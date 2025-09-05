using NoteTaking.Domain.Requests.Folder;
using NoteTaking.Domain.Responses.Folder;

namespace NoteTaking.Core.Abstractions.Services;

/// <summary>
///     Сервис для работы с папками
/// </summary>
public interface IFolderService
{
    /// <summary>
    ///     Получение папок из базы
    /// </summary>
    /// <param name="id">Идентификатор папки</param>
    /// <param name="page">Страница для пагинации</param>
    /// <param name="limit">Лимит пагинации</param>
    /// <returns></returns>
    Task<List<FolderDescriptionResponse>> Get(Guid? id, int page = 0, int limit = 20);
    
    /// <summary>
    ///     Создание папки
    /// </summary>
    /// <param name="request">Данные о создании папки</param>
    /// <returns></returns>
    Task<FolderDescriptionResponse> Create(PostFolderRequest request);
    
    /// <summary>
    ///     Обновление папки
    /// </summary>
    /// <param name="request">Данные об изменении папки</param>
    /// <returns></returns>
    Task<FolderDescriptionResponse> Update(PatchFolderRequest request);
    
    /// <summary>
    ///     Удаление папки
    /// </summary>
    /// <param name="id">Идентификатор папки</param>
    /// <returns></returns>
    Task<FolderDescriptionResponse> Delete(Guid id);
}