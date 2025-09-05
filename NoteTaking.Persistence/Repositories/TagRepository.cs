using Microsoft.EntityFrameworkCore;
using NoteTaking.Core.Abstractions.Repositories;
using NoteTaking.Domain.Entities;

namespace NoteTaking.Persistence.Repositories;

/// <inheritdoc cref="IBaseRepository{Tag}"/>
public class TagRepository : IBaseRepository<Tag>
{
    private readonly NoteTakingContext _dbContext;

    public TagRepository(NoteTakingContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // Получение всех записей
    public IQueryable<Tag> GetAll() => _dbContext.Tags;
    
    // Получение по ID
    public async Task<Tag?> GetById(Guid id) => await _dbContext.Tags.FirstOrDefaultAsync(s => s.Id == id);
    
    // Создание новой сущности
    public async Task<Tag> Create(Tag entity)
    {
        _dbContext.Tags.Add(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Изменение существующей сущности
    public async Task<Tag> Update(Tag entity)
    {
        _dbContext.Tags.Update(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Удаление сущности
    public async Task<Tag> Delete(Tag entity)
    {
        _dbContext.Tags.Remove(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Асинхронный вспомогательный метод сохранения
    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}