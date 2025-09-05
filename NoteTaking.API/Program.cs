using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NoteTaking.Core.Abstractions.Repositories;
using NoteTaking.Core.Abstractions.Services;
using NoteTaking.Core.Implementations.Services;
using NoteTaking.Core.Options;
using NoteTaking.Domain.Entities;
using NoteTaking.Persistence;
using NoteTaking.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration; // Получение доступа к конфигурации

builder.Services.AddControllers(); // Регистрирует сервисы для работы с контроллерами
builder.Services.AddSwaggerGen(); // Генерация документации Swagger

// Подключение JWT-токена через конфигурацию
builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("JwtSettings"));

// Добавление сервиса авторизации в контейнер зависимостей
builder.Services.AddAuthorization();
// Добавление сервиса аутентификации с использованием JWT Bearer токена
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Создание временного поставщика сервисов для получение зарегистрированных служб
        var serviceProvider = builder.Services.BuildServiceProvider();
        // Получение настроек класса AuthOptions из контейнера зависимостей
        var authOptions = serviceProvider.GetRequiredService<AuthOptions>();
        
        // Настройка параметров валидации JWT-токена
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = authOptions.Issuer,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = authOptions.Audience,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddTransient<IFolderService, FolderService>();
builder.Services.AddTransient<INoteService, NoteService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddDbContext<NoteTakingContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("Psql")));
builder.Services.AddTransient<IBaseRepository<User>, UserRepository>();
builder.Services.AddTransient<IBaseRepository<Tag>, TagRepository>();
builder.Services.AddTransient<IBaseRepository<Folder>, FolderRepository>();
builder.Services.AddTransient<IBaseRepository<Note>, NoteRepository>();

var app = builder.Build();

app.MapControllers(); // Машрутизация контроллеров
app.UseSwagger(); // Включает middleware для генерации Swagger JSON
app.UseSwaggerUI(); // Включает веб-интерфейс для документации API
app.UseHttpsRedirection(); // Перенаправляет HTTP запросы на HTTPS

app.Run();
