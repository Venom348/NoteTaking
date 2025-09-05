using Microsoft.AspNetCore.Mvc;
using NoteTaking.Core.Abstractions.Services;
using NoteTaking.Core.Exceptions;
using NoteTaking.Domain.Requests.Note;

namespace NoteTaking.API.Controllers;

/// <summary>
///     Контроллер заметок
/// </summary>
[ApiController]
[Route("api/notes")]
public class NotesController : Controller
{
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(Guid? id, int page = 0, int limit = 20)
    {
        try
        {
            var response = await _noteService.Get(id, page, limit);
            return Ok(response);
        }
        catch (NoteException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("Неизвестная ошибка");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostNoteRequest request)
    {
        var response = await _noteService.Create(request);
        return Ok(response);
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] PatchNoteRequest request)
    {
        try
        {
            var response = await _noteService.Update(request);
            return Ok(response);
        }
        catch (NoteException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("Неизвестная ошибка");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var response = await _noteService.Delete(id);
            return Ok(response);
        }
        catch (NoteException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("Неизвестная ошибка");
        }
    }
}