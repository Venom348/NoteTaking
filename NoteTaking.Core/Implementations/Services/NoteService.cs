using Microsoft.EntityFrameworkCore;
using NoteTaking.Core.Abstractions.Repositories;
using NoteTaking.Core.Abstractions.Services;
using NoteTaking.Core.Exceptions;
using NoteTaking.Domain.Entities;
using NoteTaking.Domain.Requests.Note;
using NoteTaking.Domain.Responses;
using NoteTaking.Domain.Responses.Note;

namespace NoteTaking.Core.Implementations.Services;

/// <inheritdoc cref="INoteService"/>
public class NoteService : INoteService
{
    private readonly IBaseRepository<Note> _noteRepository;

    public NoteService(IBaseRepository<Note> noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<List<NoteDescriptionResponse>> Get(Guid? id, int page = 0, int limit = 20)
    {
        // Если передан ID, то возвращает одну заметку
        if (id.HasValue)
        {
            var result = await _noteRepository.GetById(id.Value);
            
            // Если ID не найден, то выбрасывает исключение
            if (result is null)
            {
                throw new NoteException("Заметка с переданным ID не найдена. Попробуйте создать заметку.");
            }
            
            // Возвращает заметку в виде списка из одного элемента
            return new List<NoteDescriptionResponse>([new NoteDescriptionResponse
            {
                Id = result.Id,
                Title = result.Title,
                Content = result.Content,
                User = result.User,
                Folder = result.Folder,
                Status = result.Status,
                IsFavorites = result.IsFavorites,
                DateCreated = result.DateCreated,
                DateModified = result.DateModified,
                IsPublic = result.IsPublic
            }]);
        }
        
        // Если ID не передан, то возвращает список всех заметок с пагинацией
        var queryResult = await _noteRepository.GetAll()
            .Skip(page * limit)
            .Take(limit)
            .ToListAsync();
        // Если результат пустой, выбрасывает исключение
        if (queryResult.Count == 0)
        {
            throw new NoteException("Результат не найден. Попробуйте создать заметку/заметки");
        }
        
        // Возвращает список всех заметок
        return new List<NoteDescriptionResponse>(queryResult.Select(s => new NoteDescriptionResponse
        {
            Id = s.Id,
            Title = s.Title,
            Content = s.Content,
            User = s.User,
            Folder = s.Folder,
            Status = s.Status,
            IsFavorites = s.IsFavorites,
            DateCreated = s.DateCreated,
            DateModified = s.DateModified,
            IsPublic = s.IsPublic
        }));
    }

    public async Task<NoteDescriptionResponse> Create(PostNoteRequest request)
    {
        // Создание заметки с переданными данными
        var result = await _noteRepository.Create(new Note
        {
            Title = request.Title,
            Content = request.Content,
            User = request.User,
            Folder = request.Folder,
            Status = request.Status,
            IsFavorites = request.IsFavorites,
            DateCreated = DateTime.UtcNow,
            DateModified = request.DateModified,
            IsPublic = request.IsPublic
        });
        
        // Возвращает информацию о создании заметки
        return new NoteDescriptionResponse
        {
            Id = result.Id,
            Title = request.Title,
            Content = request.Content,
            User = request.User,
            Folder = request.Folder,
            Status = result.Status,
            IsFavorites = request.IsFavorites,
            DateCreated = DateTime.UtcNow,
            DateModified = request.DateModified,
            IsPublic = request.IsPublic
        };
    }

    public async Task<NoteDescriptionResponse> Update(PatchNoteRequest request)
    {
        // Проверка существования заметки, если такой нет - выбрасывает исключение
        var result = await _noteRepository.GetById(request.Id);

        if (result is null)
        {
            throw new NoteException("Заметка с переданным ID не найдена. Попробуйте создать заметку.");
        }
        
        // Обновляет поля заметки
        result.Title = request.Title;
        result.Content = request.Content;
        result.Status = request.Status;
        result.IsFavorites = request.IsFavorites;
        result.DateModified = DateTime.UtcNow;
        result.IsPublic = request.IsPublic;
        result = await _noteRepository.Update(result);
        
        // Возвращает обновлённые данные
        return new NoteDescriptionResponse
        {
            Id = result.Id,
            Title = request.Title,
            Content = request.Content,
            User = request.User,
            Folder = request.Folder,
            Status = result.Status,
            IsFavorites = request.IsFavorites,
            DateCreated = result.DateCreated,
            DateModified = result.DateModified,
            IsPublic = result.IsPublic
        };
    }

    public async Task<NoteResponse> Delete(Guid id)
    {
        // Ищет заметку по ID, если такой нет - выбрасывает исключение
        var result = await _noteRepository.GetById(id);

        if (result is null)
        {
            throw new NoteException("Заметка с переданным ID не найдена. Попробуйте создать заметку.");
        }
        
        // Удаляет заметку через паттерн "Репозиторий"
        result = await _noteRepository.Delete(result);
        
        // Возвращает информацию об удалённой заметки
        return new NoteDescriptionResponse
        {
            Id = result.Id,
            Title = result.Title,
            Content = result.Content,
            User = result.User,
            Folder = result.Folder,
            Status = result.Status,
            IsFavorites = result.IsFavorites,
            DateCreated = result.DateCreated,
            DateModified = result.DateModified,
            IsPublic = result.IsPublic
        };
    }
}