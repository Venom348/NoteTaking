using NoteTaking.Domain.Common;

namespace NoteTaking.Core.Abstractions.Repositories;

/// <summary>
///     Базовый интерфейс для взаимодействия с сущностями в БД
/// </summary>
public interface IBaseRepository<TEntity> where TEntity : Entity
{
    /// <summary>
    ///     Получаем запрос для entity
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> GetAll();
    
    /// <summary>
    ///     Получаем сущность по ID
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns></returns>
    Task<TEntity> GetById(Guid id);
    
    /// <summary>
    ///     Добавляем новую сущность
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <returns></returns>
    Task<TEntity> Create(TEntity entity);
    
    /// <summary>
    ///     Обновляем сущность
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <returns></returns>
    Task<TEntity> Update(TEntity entity);
    
    /// <summary>
    ///     Удаляем сущность
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <returns></returns>
    Task<TEntity> Delete(TEntity entity);
    
    Task<int> SaveChangesAsync();
}