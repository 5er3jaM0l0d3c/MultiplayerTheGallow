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
            return context.Player.ToList();
        }

        public Player GetPlayer(int id)
        {
            return context.Player.FirstOrDefault(x => x.Id == id);
        }

        public void AddPlayer(Player player)
        {
            context.Player.Add(player);
            context.SaveChanges();
        }

        public void UpdatePlayer(Player player)
        {
            context.Player.Update(player);
            context.SaveChanges();
        }

        public void DeletePlayer(int id)
        {
            var player = context.Player.FirstOrDefault(x => x.Id == id);
            context.Player.Remove(player);
        }
    }
}
