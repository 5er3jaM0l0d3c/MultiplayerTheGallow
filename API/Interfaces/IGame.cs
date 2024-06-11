using Entities;

namespace API.Interfaces
{
    public interface IGame
    {
        public List<Game> GetGames();
        public Game GetGame(int id);
        public void AddGame(Game game);
        public void UpdateGame(Game game);
        public void DeleteGame(int id);
    }
}
