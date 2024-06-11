using API.Interfaces;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private IGame Game { get; set; }
        public GameController(IGame game) 
        {
            Game = game;
        }

        [HttpGet("GetGames")]
        public List<Game> GetGames()
        {
            return Game.GetGames();
        }

        [HttpGet("GetGame")]
        public Game GetGame(int id)
        {
            return Game.GetGame(id);
        }

        [HttpPost("AddGame")]
        public void AddGame([FromBody]Game game)
        {
            Game.AddGame(game);
        }

        [HttpPut("UpdateGame")]
        public void UpdateGame([FromBody] Game game)
        {
            Game.UpdateGame(game);
        }

        [HttpDelete("DeleteGame")]
        public void DeleteGame(int id)
        {
            Game.DeleteGame(id);
        }
    }
}
