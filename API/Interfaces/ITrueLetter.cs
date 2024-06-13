using Entities;

namespace API.Interfaces
{
    public interface ITrueLetter
    {
        public List<TrueLetter> GetTrueLetters(int GameId);
        public void AddTrueLetter(TrueLetter trueLetter);
    }
}
