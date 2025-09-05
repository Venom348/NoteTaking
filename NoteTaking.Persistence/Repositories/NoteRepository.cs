using Microsoft.EntityFrameworkCore;
using NoteTaking.Core.Abstractions.Repositories;
using NoteTaking.Domain.Entities;

namespace NoteTaking.Persistence.Repositories;

/// <inheritdoc cref="IBaseRepository{Note}"/>
public class NoteRepository : IBaseRepository<Note>
{
    private readonly NoteTakingContext _dbContext;

    public NoteRepository(NoteTakingContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Получение всех записей
    public IQueryable<Note> GetAll() => _dbContext.Notes;

    // Получение по ID
    public async Task<Note?> GetById(Guid id) => await _dbContext.Notes.FirstOrDefaultAsync(s => s.Id == id);

    // Создание новой сущности
    public async Task<Note> Create(Note entity)
    {
        _dbContext.Notes.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    // Изменение существующей сущности
    public async Task<Note> Update(Note entity)
    {
        _dbContext.Notes.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    // Удаление сущности
    public async Task<Note> Delete(Note entity)
    {
        _dbContext.Notes.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    // Асинхронный вспомогательный метод сохранения
    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}