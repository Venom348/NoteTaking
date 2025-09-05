using Microsoft.EntityFrameworkCore;
using NoteTaking.Core.Abstractions.Repositories;
using NoteTaking.Core.Abstractions.Services;
using NoteTaking.Core.Exceptions;
using NoteTaking.Domain.Entities;
using NoteTaking.Domain.Requests.User;
using NoteTaking.Domain.Responses;
using NoteTaking.Domain.Responses.User;

namespace NoteTaking.Core.Implementations.Services;

/// <inheritdoc cref="IUserService"/>
public class UserService : IUserService
{
    private readonly IBaseRepository<User> _userRepository;

    public UserService(IBaseRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDescriptionResponse>> Get(Guid? id, int page = 0, int limit = 20)
    {
        // Если передан ID, то возвращает одного пользователя
        if (id.HasValue)
        {
            var result = await _userRepository.GetById(id.Value);
            
            // Если ID не найден, то выбрасывает исключение
            if (result is null)
            {
                throw new UserException("Пользователь с переданным ID не найден. Попробуйте зарегистрироваться.");
            }
            
            // Возвращает пользователя в виде списка из одного элемента
            return new List<UserDescriptionResponse>([ new UserDescriptionResponse
            {
                Id = result.Id,
                Email = result.Email,
                FirstName = result.FirstName,
                LastName = result.LastName,
                DateRegistration = result.DateRegistration,
            }]);
        }
        
        // Если ID не передан, то возвращает список всех пользователей с пагинацией
        var queryResult = await _userRepository.GetAll()
            .Skip(page * limit)
            .Take(limit)
            .ToListAsync();
        // Если результат пустой, выбрасывает исключение
        if (queryResult.Count == 0)
        {
            throw new UserException("Результат не найден. Попробуйте зарегистрироваться или зарегистрировать несколько пользователей");
        }
        
        // Возвращает список всех пользователей
        return new List<UserDescriptionResponse>(queryResult.Select(s => new UserDescriptionResponse
        {
            Id = s.Id,
            Email = s.Email,
            FirstName = s.FirstName,
            LastName = s.LastName,
            DateRegistration = s.DateRegistration
        }));
    }
    
    public async Task<UserDescriptionResponse> Update(PatchUserRequest request)
    {
        // Проверка существования пользователя, если такого нет - выбрасывает исключение
        var result = await _userRepository.GetById(request.Id);

        if (result is null)
        {
            throw new UserException("Пользователь с переданным ID не найден. Попробуйте зарегистрироваться.");
        }
        
        // Обновляет поля пользователя
        result.Email = request.Email;
        result.Password = request.Password;
        result.FirstName = request.FirstName;
        result.LastName = request.LastName;
        result = await _userRepository.Update(result);
        
        // Возвращает обновлённые данные
        return new UserDescriptionResponse
        {
            Id = result.Id,
            Email = result.Email,
            FirstName = result.FirstName,
            LastName = result.LastName,
            DateRegistration = result.DateRegistration,
        };
    }

    public async Task<UserResponse> Delete(Guid id)
    {
        // Ищет пользователя по ID, если такого нет - выбрасывает исключение
        var result = await _userRepository.GetById(id);

        if (result is null)
        {
            throw new UserException("Пользователь с переданным ID не найден. Попробуйте зарегистрироваться.");
        }
        
        // Удаляет пользователя через паттер "Репозиторий"
        result = await _userRepository.Delete(result);
        
        // Возвращает информацию об удалённом пользователе
        return new UserDescriptionResponse
        {
            Id = result.Id,
            Email = result.Email,
            FirstName = result.FirstName,
            LastName = result.LastName,
            DateRegistration = result.DateRegistration,
        };
    }
}