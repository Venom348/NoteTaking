using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NoteTaking.Core.Options;

/// <summary>
///     Класс для настройки генерации JWT-токена
/// </summary>
public class AuthOptions
{
    /// <summary>
    ///     Издатель токена
    /// </summary>
    public string Issuer { get; set; } = string.Empty;
    
    /// <summary>
    ///     Потребитель токена
    /// </summary>
    public string Audience { get; set; } = string.Empty;
    
    /// <summary>
    ///     Ключ для шифрации
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;
    
    /// <summary>
    ///     Метод получения шифрованного ключа
    /// </summary>
    public SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
}