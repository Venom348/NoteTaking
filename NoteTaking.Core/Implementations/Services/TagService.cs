using Microsoft.EntityFrameworkCore;
using NoteTaking.Core.Abstractions.Repositories;
using NoteTaking.Core.Abstractions.Services;
using NoteTaking.Core.Exceptions;
using NoteTaking.Domain.Entities;
using NoteTaking.Domain.Requests.Tag;
using NoteTaking.Domain.Responses;
using NoteTaking.Domain.Responses.Tag;

namespace NoteTaking.Core.Implementations.Services;

/// <inheritdoc cref="ITagService"/>
public class TagService : ITagService
{
    private readonly IBaseRepository<Tag> _tagRepository;

    public TagService(IBaseRepository<Tag> tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<List<TagDescriptionResponse>> Get(Guid? id, int page = 0, int limit = 20)
    {
        // Если передан ID, то возвращает один тег
        if (id.HasValue)
        {
            var result = await _tagRepository.GetById(id.Value);
            
            // Если ID не найден, то выбрасывает исключение
            if (result is null)
            {
                throw new TagException("Тег с переданным ID не найден. Попробуйте создать тег.");
            }
            
            // Возвращает тег в виде списка из одного элемента
            return new List<TagDescriptionResponse>([new TagDescriptionResponse
            {
                Id = result.Id,
                Name = result.Name,
                Color = result.Color,
                User = result.User,
                DateCreated = result.DateCreated,
            }]);
        }
        
        // Если ID не передан, то возвращает список всех тегов с пагинацией
        var queryResult = await _tagRepository.GetAll()
            .Skip(page * limit)
            .Take(limit)
            .ToListAsync();
        // Если результат пустой, выбрасывает исключение
        if (queryResult.Count == 0)
        {
            throw new TagException("Результат не найден. Попробуйте создать тег/теги");
        }

        // Возвращает список всех тегов
        return new List<TagDescriptionResponse>(queryResult.Select(s => new TagDescriptionResponse
        {
            Id = s.Id,
            Name = s.Name,
            Color = s.Color,
            User = s.User,
            DateCreated = s.DateCreated
        }));
    }

    public async Task<TagDescriptionResponse> Create(PostTagRequest request)
    {
        // Создание тега с переданными данными
        var result = await _tagRepository.Create(new Tag
        {
            Name = request.Name,
            Color = request.Color,
            User = request.User,
            DateCreated = DateTime.UtcNow
        });
        
        // Возвращает информацию о создании тега
        return new TagDescriptionResponse
        {
            Id = result.Id,
            Name = result.Name,
            Color = result.Color,
            User = result.User,
            DateCreated = result.DateCreated
        };
    }

    public async Task<TagDescriptionResponse> Update(PatchTagRequest request)
    {
        // Проверка существования тега, если такого нет - выбрасывает исключение
        var result = await _tagRepository.GetById(request.Id);

        if (result is null)
        {
            throw new TagException("Тег с переданным ID не найден. Попробуйте создать тег.");
        }
        
        // Обновляет поля тега
        result.Name = request.Name;
        result.Color = request.Color;
        result = await _tagRepository.Update(result);
        
        // Возвращает обновлённые данные
        return new TagDescriptionResponse
        {
            Id = result.Id,
            Name = result.Name,
            Color = result.Color,
            User = result.User,
            DateCreated = result.DateCreated
        };
    }

    public async Task<TagResponse> Delete(Guid id)
    {
        // Ищет тег по ID, если такого нет - выбрасывает исключение
        var result = await _tagRepository.GetById(id);

        if (result is null)
        {
            throw new TagException("Тег с переданным ID не найден. Попробуйте создать тег.");
        }
        
        // Удаляет тег через паттерн "Репозиторий"
        result = await _tagRepository.Delete(result);
        
        // Возвращает информацию об удалённом теге
        return new TagDescriptionResponse
        {
            Id = result.Id,
            Name = result.Name,
            Color = result.Color,
            User = result.User,
            DateCreated = result.DateCreated
        };
    }
}