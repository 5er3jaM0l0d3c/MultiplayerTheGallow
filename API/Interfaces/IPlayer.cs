using Entities;

namespace API.Interfaces
{
    public interface IPlayer
    {
        public List<Player> GetPlayers();
        public Player GetPlayer(int id);
        public Player AutorizePlayer(string Login, string Password);
        public void AddPlayer(Player player);
        public void UpdatePlayer(Player player);
        public void DeletePlayer(int id);
    }
}
