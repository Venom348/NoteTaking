using Microsoft.AspNetCore.Mvc;
using NoteTaking.Core.Abstractions.Services;
using NoteTaking.Core.Exceptions;
using NoteTaking.Domain.Requests.Folder;

namespace NoteTaking.API.Controllers;

/// <summary>
///     Контроллер папок
/// </summary>
[ApiController]
[Route("api/folders")]
public class FoldersController : Controller
{
    private readonly IFolderService _folderService;

    public FoldersController(IFolderService folderService)
    {
        _folderService = folderService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid? id, int page = 0, int limit = 20)
    {
        try
        {
            var response = await _folderService.Get(id, page, limit);
            return Ok(response);
        }
        catch (FolderException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("Неизвестная ошибка");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostFolderRequest request)
    {
        var response = await _folderService.Create(request);
        return Ok(response);
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] PatchFolderRequest request)
    {
        try
        {
            var response = await _folderService.Update(request);
            return Ok(response);
        }
        catch (FolderException ex)
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
            var response = await _folderService.Delete(id);
            return Ok(response);
        }
        catch (FolderException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("Неизвестная ошибка");
        }
    }
}