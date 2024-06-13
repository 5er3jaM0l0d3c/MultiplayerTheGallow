using API.Interfaces;
using Entities;
using Player = Entities.Player;

namespace API.Services
{
    public class PlayerServices:IPlayer
    {
        private TheGallowContext context { get; set; }
        public PlayerServices(TheGallowContext context)
        {
            this.context = context;
        }

        public List<Player> GetPlayers()
        {
            return context.Players.ToList();
        }

        public Player GetPlayer(int id)
        {
            return context.Players.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Данного пользователя не существует.");
        }

        public void AddPlayer(Player player)
        {
            context.Players.Add(player);
            context.SaveChanges();
        }

        public void UpdatePlayer(Player player)
        {
            context.Players.Update(player);
            context.SaveChanges();
        }

        public void DeletePlayer(int id)
        {
            var player = context.Players.FirstOrDefault(x => x.Id == id);
            try
            {
                context.Players.Remove(player);
                context.SaveChanges();
            }
            catch 
            {
                throw new Exception("Данного пользователя не существует.");
            }
        }

        public Player AutorizePlayer(string Login, string Password)
        {
            var player = context.Players.FirstOrDefault(x => x.Login == Login && x.Password == Password);
            return player ?? throw new Exception("Неправильный логин или пароль.");
        }
    }
}
