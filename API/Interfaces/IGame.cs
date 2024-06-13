using Entities;

namespace API.Interfaces
{
    public interface IGame
    {
        public List<Game> GetGames();
        public Game GetGame(int id);
        public void AddGame(int MakerId);
        public void UpdateGame(Game game);
        public void DeleteGame(int id);
        //---------------------------------------------- <<<< ГРАНИЦА НЕ ДЕФОЛТНЫХ СЕРВИСОВ >>>> ---------------------------------------------------
        public bool ConnectMaker(int MakerId, int GameId = -1);
        public void SetGame(int gameId, string word, int numOfMistakes);
        public bool CheckLetter(int GameId, string Letter);
        public List<string> FetchTrueLetters(int GameId);
        public bool IsMakerConnected(int GameId);
        public string GetWord(int GameId);
        public int GetMistakes(int GameId);

    }
}
