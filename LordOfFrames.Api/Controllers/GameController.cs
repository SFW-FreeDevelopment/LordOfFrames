using LordOfFrames.Database;
using LordOfFrames.Models;
using LordOfFrames.Models.Request;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GetAllGames()
    {
        return Ok(await _repository.GetAllGames());
    }
    
    [HttpGet("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Game))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGameById([FromRoute] string id)
    {
        return Ok(await _repository.GetGameById(id));
    }
    
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, null, typeof(Game))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateGame(GameCreateRequest request)
    {
        var game = new Game(request);
        return Created($"/games/{game.Id}", await _repository.CreateGame(game));
    }

    [HttpPatch("{id}/updateGameInformation")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Game))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateGame([FromRoute] string id, GameUpdateRequest request)
    {
        var game = await _repository.GetGameById(id);
        return Ok(await _repository.UpdateGame(id, game));
    }
    
    [HttpGet("{id}/characters")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(List<Character>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> GetAllCharacters([FromRoute] string id)
    {
        return Ok(await _repository.GetAllCharactersByGameId(id));
    }
    
    [HttpGet("{id}/characters/{slug}")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Character))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> GetCharacterBySlug([FromRoute] string id, [FromRoute] string slug)
    {
        return Ok(await _repository.GetCharacterByGameIdAndSlug(id, slug));
    }
    
    [HttpPatch("{id}/characters/addCharacter")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Character))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddCharacter([FromRoute] string id, Character request)
    {
        return Ok(await _repository.CreateCharacter(id, request));
    }
    
    [HttpPatch("{id}/characters/{slug}/updateCharacterInformation")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Game))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateCharacter([FromRoute] string id, [FromRoute] string slug, Character request)
    {
        return Ok(await _repository.UpdateCharacter(id, request));
    }
    
    [HttpGet("{id}/systemMechanics")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(List<SystemMechanic>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> GetAllSystemMechanics([FromRoute] string id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}/systemMechanics/{slug}")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Character))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> GetSystemMechanicBySlug([FromRoute] string id, [FromRoute] string slug)
    {
        throw new NotImplementedException();
    }
    
    [HttpPatch("{id}/characters/addSystemMechanic")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Character))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddSystemMechanic([FromRoute] string id, SystemMechanic request)
    {
        throw new NotImplementedException();
    }
    
    [HttpPatch("{id}/systemMechanics/{slug}/updateSystemMechanicInformation")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Game))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateSystemMechanic([FromRoute] string id, [FromRoute] string slug, SystemMechanic request)
    {
        throw new NotImplementedException();
    }
}