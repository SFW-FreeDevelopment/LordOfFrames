using LordOfFrames.Database;
using Microsoft.AspNetCore.Mvc;

namespace LordOfFrames.Web.Controllers;

[Route("games")]
public class GameController : Controller
{
    private readonly GameRepository _gameRepository;
    
    public GameController(GameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    
    public async Task<IActionResult> ViewAll()
    {
        var games = await _gameRepository.GetAllGames();
        return games != null ? View(games.ToList()) : RedirectToAction("Index", "Home");
    }
    
    [Route("{id}")]
    public async Task<IActionResult> ViewSingle(string id)
    {
        var game = await _gameRepository.GetGameById(id);
        return game != null ? View(game) : RedirectToAction("Index", "Home");
    }
}