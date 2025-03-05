using API.Interfaces;
using Entities;

namespace API.Services
{
    public class TrueLetterServices : ITrueLetter
    {
        private TheGallowContext context {  get; set; }
        public TrueLetterServices(TheGallowContext context)
        {
            this.context = context;
        }

        public List<TrueLetter> GetTrueLetters(int GameId)
        {
            return context.TrueLetter.Where(x => x.GameId == GameId).ToList();
        }

        public void AddTrueLetter(TrueLetter mistake)
        {
            context.TrueLetter.Add(mistake);
            context.SaveChanges();
        }
    }
}
