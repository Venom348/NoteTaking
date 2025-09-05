using Microsoft.AspNetCore.Mvc;
using NoteTaking.Core.Abstractions.Services;
using NoteTaking.Core.Exceptions;
using NoteTaking.Domain.Requests.User;

namespace NoteTaking.API.Controllers;

/// <summary>
///     Контроллер аутентификации
/// </summary>
[ApiController]
[Route("api/authentication")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] PostUserRequest request)
    {
        try
        {
            await _authService.Register(request);
            return Ok();
        }
        catch (UserException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("Неизвестная ошибка");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] PostUserRequest request)
    {
        try
        {
            var response = await _authService.Login(request);
            return Ok(response);
        }
        catch (UserException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("Неизвестная ошибка");
        }
    }
}