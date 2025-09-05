using Microsoft.EntityFrameworkCore;
using NoteTaking.Core.Abstractions.Repositories;
using NoteTaking.Core.Abstractions.Services;
using NoteTaking.Core.Exceptions;
using NoteTaking.Domain.Entities;
using NoteTaking.Domain.Requests.Folder;
using NoteTaking.Domain.Responses.Folder;

namespace NoteTaking.Core.Implementations.Services;

/// <inheritdoc cref="IFolderService"/>
public class FolderService : IFolderService
{
    private readonly IBaseRepository<Folder> _folderRepository;

    public FolderService(IBaseRepository<Folder> folderRepository)
    {
        _folderRepository = folderRepository;
    }

    public async Task<List<FolderDescriptionResponse>> Get(Guid? id, int page = 0, int limit = 20)
    {
        // Если передан ID, то возвращает одну папку
        if (id.HasValue)
        {
            var result = await _folderRepository.GetById(id.Value);
            
            // Если ID не найдет, то выбрасывает исключение
            if (result is null)
            {
                throw new FolderException("Папка с указанным ID не найдена. Попробуйте создать папку.");
            }
            
            // Возвращает папку в виде списка из одного элемента
            return new List<FolderDescriptionResponse>([new FolderDescriptionResponse
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                User = result.User,
                DateCreated = result.DateCreated
            }]);
        }
        
        // Если ID не передан, то возвращает список всех папок с пагинацией
        var queryResult = await _folderRepository.GetAll()
            .Skip(page * limit)
            .Take(limit)
            .ToListAsync();
        // Если результат пустой, выбрасывает исключение
        if (queryResult.Count == 0)
        {
            throw new FolderException("Результат не найдет. Попробуйте создать папку/папки.");
        }
        
        // Возвращает список всех папок
        return new List<FolderDescriptionResponse>(queryResult.Select(s => new FolderDescriptionResponse
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description,
            User = s.User,
            DateCreated = s.DateCreated
        }));
    }

    public async Task<FolderDescriptionResponse> Create(PostFolderRequest request)
    {
        // Создание папки с переданными данными
        var result = await _folderRepository.Create(new Folder
        {
            Name = request.Name,
            Description = request.Description,
            User = request.User,
            DateCreated = DateTime.UtcNow
        });
        
        // Возвращает информацию о создании папки
        return new FolderDescriptionResponse
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
            User = result.User,
            DateCreated = result.DateCreated
        };
    }

    public async Task<FolderDescriptionResponse> Update(PatchFolderRequest request)
    {
        // Проверка существования папки, если такой нет - выбрасывает исключение
        var result = await _folderRepository.GetById(request.Id);
        
        if (result is null)
        {
            throw new FolderException("Папка с переданным ID не найдена. Попробуйте создать папку.");
        }
        
        // Обновляем поля папки
        result.Name = request.Name;
        result.Description = request.Description;
        result = await _folderRepository.Update(result);
        
        // Возвращаем обновлённые данные
        return new FolderDescriptionResponse
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
            User = result.User,
            DateCreated = result.DateCreated
        };
    }

    public async Task<FolderDescriptionResponse> Delete(Guid id)
    {
        // Ищет папку по ID, если такой нет - выбрасывает исключение
        var result = await _folderRepository.GetById(id);

        if (result is null)
        {
            throw new FolderException("Папка с переданным ID не найдена. Попробуйте создать папку.");
        }
        
        // Удаляет папку через паттерн "Репозиторий"
        result = await _folderRepository.Delete(result);
        
        // Возвращает информацию об удалённой папки
        return new FolderDescriptionResponse
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
            User = result.User,
            DateCreated = result.DateCreated
        };
    }
}