namespace NoteTaking.Domain.Responses.User;

/// <summary>
///     Класс для представления информации о пользователе
/// </summary>
public class UserDescriptionResponse : UserResponse
{
    /// <summary>
    ///     Email пользователя
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    ///     Имя пользователя
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    ///     Фамилия пользователя
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    ///     Дата регистрации
    /// </summary>
    public DateTime DateRegistration { get; set; }
}