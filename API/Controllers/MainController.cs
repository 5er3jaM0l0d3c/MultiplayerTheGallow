using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        Interface Game { get; set; }
        public MainController(Interface iinterface) 
        {
            Game = iinterface;
        }



        [HttpGet(nameof(CheckLetter))]
        public List<int> CheckLetter(string letter)
        {
            return Game.CheckLetter(letter);
        }

        [HttpGet(nameof(GetWord))]
        public string GetWord()
        {
            return Game.GetWord();
        }

        


        [HttpGet(nameof(TakeLastLetter))]
        public string TakeLastLetter()
        {
            return Game.TakeLastLetter();
        }

        [HttpGet(nameof(WriteLastLetter))]
        public void WriteLastLetter(char letter)
        {
            Game.WriteLastLetter(letter);
        }

        [HttpPost(nameof(CheckWord))]
        public void CheckWord(string word)
        {
            Game.CheckWord(word);
        }

        [HttpGet(nameof(GetNumOfMistakes))]
        public int GetNumOfMistakes()
        {
            return Game.GetNumOfMistakes();
        }

        [HttpGet(nameof(ReloadAll))]
        public void ReloadAll()
        {
            Game.ReloadAll();
        }

        [HttpGet("CanTakeRole")]
        public bool CanTakeRole(Player.Roles role)
        {
            return Game.CanTakeRole(role);
        }

        [HttpGet("CheckConnection")]
        public int CheckConnection(int num)
        {
            return Game.CheckConnection(num);
        }

        [HttpGet("CreatePlayer")]
        public void CreatePlayer(Player.Roles role)
        {
            Game.CreatePlayer(role);
        }

        [HttpGet("IsWaitingPlayer")]
        public bool IsWaitingPlayer(Player.Roles role)
        {
            return Game.IsWaitingPlayer(role);
        }

        [HttpGet("MakeSecret")]
        public void MakeSecret(string word) 
        {
            Game.MakeSecret(word);
        }

        [HttpGet("SetNumOfMistakes")]
        public void SetNumOfMistakes(int num)
        {
            Game.SetNumOfMistakes(num);
        }
    }

}     