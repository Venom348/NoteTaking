using Microsoft.EntityFrameworkCore;
using NoteTaking.Domain.Entities;

namespace NoteTaking.Persistence;

/// <summary>
///     Модель для подключения БД (PostgreSQL)
/// </summary>
public class NoteTakingContext : DbContext
{
    // Определение сущностей
    public DbSet<User> Users => Set<User>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Folder> Folders => Set<Folder>();
    
    public NoteTakingContext(DbContextOptions options) : base(options)
    {
        // Проверяет, существует ли БД
        // Если нет, то создаёт её
        // Если существует - возвращает false
        if (Database.EnsureCreated())
        {
            Init();
        }
    }

    private void Init()
    {
        SaveChanges();
    }
}