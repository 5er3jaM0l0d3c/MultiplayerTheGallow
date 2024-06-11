using API.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class GameServices : IGame
    {
        private TheGallowContext context { get; set; }
        public GameServices(TheGallowContext context)
        {
            this.context = context;
        }

        public void AddGame(Game game)
        {
            context.Add(game);
            context.SaveChanges();
        }

        public void DeleteGame(int id)
        {
            var game = context.Game.FirstOrDefault(x => x.Id == id);
            context.Remove(game);
            context.SaveChanges();
        }

        public Game GetGame(int id)
        {
            return context.Game.Include(x => x.Destroyer).Include(x => x.Maker).FirstOrDefault(x => x.Id == id);
        }

        public List<Game> GetGames()
        {
            return context.Game.Include(x => x.Destroyer).Include(x => x.Maker).ToList();
        }

        public void UpdateGame(Game game)
        {
            context.Update(game);
            context.SaveChanges();
        }
    }
}
