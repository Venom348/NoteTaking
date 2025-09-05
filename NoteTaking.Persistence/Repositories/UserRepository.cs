using Microsoft.EntityFrameworkCore;
using NoteTaking.Core.Abstractions.Repositories;
using NoteTaking.Domain.Entities;

namespace NoteTaking.Persistence.Repositories;

///<inheritdoc cref="IBaseRepository{User}"/>
public class UserRepository : IBaseRepository<User>
{
    private readonly NoteTakingContext _dbContext;

    public UserRepository(NoteTakingContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // Получение всех записей
    public IQueryable<User> GetAll() => _dbContext.Users;
    
    // Получение по ID
    public async Task<User?> GetById(Guid id) => await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == id);
    
    // Создание новой сущности
    public async Task<User> Create(User entity)
    {
        _dbContext.Users.Add(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Изменение существующей сущности
    public async Task<User> Update(User entity)
    {
        _dbContext.Users.Update(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Удаление сущности
    public async Task<User> Delete(User entity)
    {
        _dbContext.Users.Remove(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Асинхронный вспомогательный метод сохранения
    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}