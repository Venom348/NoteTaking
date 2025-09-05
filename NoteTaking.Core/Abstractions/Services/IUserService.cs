using NoteTaking.Domain.Requests.User;
using NoteTaking.Domain.Responses;
using NoteTaking.Domain.Responses.User;

namespace NoteTaking.Core.Abstractions.Services;

/// <summary>
///     Сервис для работы с пользователями
/// </summary>
public interface IUserService
{
    /// <summary>
    ///     Получение пользователей из базы
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="page">Страница для пагинации</param>
    /// <param name="limit">Лимит пагинации</param>
    /// <returns></returns>
    Task<List<UserDescriptionResponse>> Get(Guid? id, int page = 0, int limit = 20);
    
    /// <summary>
    ///     Обновление данных пользователя
    /// </summary>
    /// <param name="request">Данные об обновлении пользователя</param>
    /// <returns></returns>
    Task<UserDescriptionResponse> Update(PatchUserRequest request);
    
    /// <summary>
    ///     Удаление пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns></returns>
    Task<UserResponse> Delete(Guid id);
}