using LordOfFrames.Database;
using LordOfFrames.Models;
using LordOfFrames.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LordOfFrames.Api.Controllers;

[ApiController]
[Route("games")]
public class GameController : ControllerBase
{
    private readonly GameRepository _repository;

    public GameController(GameRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(List<Game>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repository.GetAll());
    }

    [HttpGet("id")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Game))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        return Ok(await _repository.GetById(id));
    }
    
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, null, typeof(Game))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(GameCreateRequest request)
    {
        var game = new Game(request);
        return Created($"/games/{game.Id}", await _repository.Create(game));
    }

    [HttpPut("id")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Game))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update([FromRoute] string id, Game request)
    {
        throw new NotImplementedException();
    }
}