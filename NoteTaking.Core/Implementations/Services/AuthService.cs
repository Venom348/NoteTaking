using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoteTaking.Core.Abstractions.Repositories;
using NoteTaking.Core.Abstractions.Services;
using NoteTaking.Core.Exceptions;
using NoteTaking.Core.Options;
using NoteTaking.Domain.Entities;
using NoteTaking.Domain.Requests.User;

namespace NoteTaking.Core.Implementations.Services;

/// <inheritdoc cref="IAuthService"/>
public class AuthService : IAuthService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IOptions<AuthOptions> _authOptions;
    
    public AuthService(IBaseRepository<User> userRepository, IOptions<AuthOptions> authOptions)
    {
        _userRepository = userRepository;
        _authOptions = authOptions;
    }

    public async Task Register(PostUserRequest request)
    {
        // Переданные данные для создание пользователя
        var result = new User
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateRegistration = DateTime.UtcNow,
        };
        
        // Хешированние пароля
        var passwordHash = GetHashPassword(request.Password);
        result.Password = passwordHash;
        
        // Создание пользователя
        await _userRepository.Create(result);
    }

    public async Task<string> Login(PostUserRequest request)
    {
        // Переданные данные для авторизации пользователя
        var result = await _userRepository.GetAll()
            .FirstOrDefaultAsync(w => w.Email == request.Email && w.Password == request.Password);
        
        // Хешированние пароля
        var passwordHash = GetHashPassword(request.Password);
        request.Password = passwordHash;
        
        // Проверка существования аккаунта
        if (result is null)
        {
            throw new UserException("Account not found");
        }

        var claims = new List<Claim> { new Claim(ClaimTypes.Email, request.Email) };
        // Создание JWT-токена
        var jwt = new JwtSecurityToken(
            issuer: _authOptions.Value.Issuer,
            audience: _authOptions.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)), // время действия 10 минут
            signingCredentials: new SigningCredentials(_authOptions.Value.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        
        // Возвращает JWT-токен
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
    
    /// <summary>
    ///     Метод для хешированния пароля
    /// </summary>
    /// <param name="password">Пароль пользователя</param>
    /// <returns></returns>
    private string GetHashPassword(string password)
    {
        using var sha = SHA256.Create();
        var data = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
        return Encoding.ASCII.GetString(data);
    }
}