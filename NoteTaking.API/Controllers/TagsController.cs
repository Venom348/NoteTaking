using Microsoft.AspNetCore.Mvc;
using NoteTaking.Core.Abstractions.Services;
using NoteTaking.Core.Exceptions;
using NoteTaking.Domain.Entities;
using NoteTaking.Domain.Requests.Tag;

namespace NoteTaking.API.Controllers;

/// <summary>
///     Контроллер тегов
/// </summary>
[ApiController]
[Route("api/tags")]
public class TagsController : Controller
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid? id, int page = 0, int limit = 20)
    {
        try
        {
            var response = await _tagService.Get(id, page, limit);
            return Ok(response);
        }
        catch (TagException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("Неизвестная ошибка");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostTagRequest request)
    {
        var response = await _tagService.Create(request);
        return Ok(response);
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] PatchTagRequest request)
    {
        try
        {
            var response = await _tagService.Update(request);
            return Ok(response);
        }
        catch (TagException ex)
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
            var response = await _tagService.Delete(id);
            return Ok(response);
        }
        catch (TagException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("Неизвестная ошибка");
        }
    }
}