using Microsoft.EntityFrameworkCore;
using NoteTaking.Core.Abstractions.Repositories;
using NoteTaking.Domain.Entities;

namespace NoteTaking.Persistence.Repositories;

/// <inheritdoc cref="IBaseRepository{Folder}"/>
public class FolderRepository : IBaseRepository<Folder>
{
    private readonly NoteTakingContext _dbContext;

    public FolderRepository(NoteTakingContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Получение всех записей
    public IQueryable<Folder> GetAll() => _dbContext.Folders;

    // Получение по ID
    public async Task<Folder?> GetById(Guid id) => await _dbContext.Folders.FirstOrDefaultAsync(s => s.Id == id);

    // Создание новой сущности
    public async Task<Folder> Create(Folder entity)
    {
        _dbContext.Folders.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    // Изменение существующей сущности
    public async Task<Folder> Update(Folder entity)
    {
        _dbContext.Folders.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    // Удаление сущности
    public async Task<Folder> Delete(Folder entity)
    {
        _dbContext.Folders.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    // Асинхронный вспомогательный метод сохранения
    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}