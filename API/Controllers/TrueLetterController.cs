using API.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrueLetterController : ControllerBase
    {
        private ITrueLetter TrueLetter { get; set; }
        public TrueLetterController(ITrueLetter trueLetter)
        {
            TrueLetter = trueLetter;
        }

        [HttpGet("GetTrueLetter")]
        public List<TrueLetter> GetTrueLetter(int gameId)
        {
            return TrueLetter.GetTrueLetters(gameId);
        }

        [HttpPost("AddTrueLetter")]
        public void AddTrueLetter([FromBody] TrueLetter mistake)
        {
            TrueLetter.AddTrueLetter(mistake);
        }
    }
}
