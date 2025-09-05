using NoteTaking.Domain.Requests.User;

namespace NoteTaking.Core.Abstractions.Services;

/// <summary>
///     Сервис для аутентификации пользователя
/// </summary>
public interface IAuthService
{
    /// <summary>
    ///     Регистрация пользователя
    /// </summary>
    /// <param name="request">Данные о создания пользователя</param>
    /// <returns></returns>
    Task Register(PostUserRequest request);
    
    /// <summary>
    ///     Авторизация пользователя
    /// </summary>
    /// <param name="request">Данные для авторизации пользователя</param>
    /// <returns></returns>
    Task<string> Login(PostUserRequest request);
}