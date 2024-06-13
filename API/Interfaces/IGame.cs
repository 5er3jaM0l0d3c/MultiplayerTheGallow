using Entities;

namespace API.Interfaces
{
    public interface IGame
    {
        public List<Game> GetGames();
        public Game GetGame(int id);
        public Game AddGame(int DestroyerId);
        public void UpdateGame(Game game);
        public void DeleteGame(int id);
        //---------------------------------------------- <<<< ГРАНИЦА НЕ ДЕФОЛТНЫХ СЕРВИСОВ >>>> ---------------------------------------------------
        public Game? ConnectMaker(int MakerId, int GameId = -1);
        public void SetGame(int gameId, string word, int numOfMistakes);
        public bool CheckLetter(int GameId, string Letter);
        public bool IsMakerConnected(int GameId);
        public string GetWord(int GameId);
        public int GetMistakes(int GameId);

    }
}
