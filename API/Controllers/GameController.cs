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

        [HttpGet("AddGame")]
        public int AddGame(int id)
        {
            return Game.AddGame(id);
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

        [HttpGet("SetGame")]
        public void SetGame(int gameId, string word, int numOfMistakes)
        {
            Game.SetGame(gameId, word, numOfMistakes);
        }

        [HttpGet("ConnectDestroyer")]
        public int? ConnectDestroyer(int DestroyerId, int GameId = -1)
        {
            return Game.ConnectDestroyer(DestroyerId, GameId);
        }

        [HttpGet("IsDestroyerConnected")]
        public bool IsDestroyerConnected(int GameId)
        {
            return Game.IsDestroyerConnected(GameId);
        }

        [HttpGet("CheckLetter")]
        public bool CheckLetter(int GameId, string Letter)
        {
            return Game.CheckLetter(GameId, Letter);
        }

        [HttpGet("GetWord")]
        public string GetWord(int GameId)
        {
            return Game.GetWord(GameId);
        }

        [HttpGet("GetMistakes")]
        public int GetMistakes(int GameId)
        {
            return Game.GetMistakes(GameId);
        }

        [HttpGet("IsGameSetted")]
        public bool IsGameSetted(int GameId)
        {
            return Game.IsGameSetted(GameId);
        }

        [HttpGet("GetLastLetter")]
        public string? GetLastLetter(int GameId)
        {
            return Game.GetLastLetter(GameId);
        }
    }
}
