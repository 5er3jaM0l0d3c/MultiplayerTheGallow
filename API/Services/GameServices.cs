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
        /// <summary>
        /// Создает экземпляр игровой сессии
        /// </summary>
        /// <param name="DestroyerId">Идентификатор игрока разгадываюего слово</param>
        public void AddGame(int DestroyerId)
        {
            var game = new Game();
            game.DestroyerId = DestroyerId;
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
            return context.Game.Include(x => x.Destroyer).Include(x => x.Maker).FirstOrDefault(x => x.Id == id) ?? throw new Exception("Данной игровой сессии не существует.");
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

//---------------------------------------------- <<<< ГРАНИЦА НЕ ДЕФОЛТНЫХ СЕРВИСОВ >>>> ---------------------------------------------------

        /// <summary>
        /// Добавляет в экземпляр игровой сессии слово и количество ошибок
        /// </summary>
        /// <param name="gameId">Идентификатор игры</param>
        /// <param name="word">Слово</param>
        /// <param name="numOfMistakes">Количество ошибок</param>
        public void SetGame(int gameId, string word, int numOfMistakes)
        {
            var game = GetGame(gameId);
            game.Word = word.ToLower().Replace(" ", "");
            game.Mistakes = numOfMistakes;
        }
        /// <summary>
        /// Подключение к экземпляру игровой сессии игрока разгыдывающего слово
        /// </summary>
        /// <param name="MakerId">Идентификатор игрока</param>
        /// <param name="GameId">Идентификатор игры (необязательный - добавляет в первую свободную игровую сессию)</param>
        /// <returns>true - игра найдена</returns>
        /// <returns>false - поиск игры</returns>
        /// <exception cref="Exception">Выбрасывается если игровой сессии не существует или она занята</exception>
        public bool ConnectMaker(int MakerId, int GameId = -1)
        {
            if (GameId == -1)
            {
                var game =  GetGame(GameId);
                if(game != null)
                {
                    game.MakerId = MakerId;
                    return true;
                }
                return false;
            }
            else
            {
                var game = GetGame(GameId);
                if(game.MakerId == null)
                {
                    game.MakerId= MakerId;
                    return true;
                }
                else
                {
                    throw new Exception("Данной игровой сессии не существует или она уже занята.");
                }
            }
        }


        /// <summary>
        /// Проверяет наличие буквы в слове
        /// </summary>
        /// <param name="GameId">Идентификатор игры</param>
        /// <param name="Letter">Буква</param>
        /// <returns>true - буква в наличии   false - буквы нет</returns>
        /// <exception cref="Exception">Выбрасывается если данной игровой сессии нет</exception>
        public bool CheckLetter(int GameId, string Letter)
        {
            var game = GetGame(GameId);

            if (game.Word.Any(c => char.IsLetter(Letter.First())))
            {
                game.TrueLetters.Add(Letter);
                return true;
            }

            game.Mistakes--;
            return false;
        }
        /// <summary>
        /// Возвращает верные угаданные буквы в слове в данной игровой сессии
        /// </summary>
        /// <param name="GameId">Идентификатор сессии</param>
        /// <returns>Список верных угаданных букв</returns>
        /// <exception cref="Exception">Выбрасывается если данной игровой сессии нет</exception>
        public List<string> FetchTrueLetters(int GameId)
        {
            var game = GetGame(GameId);
            
            return game.TrueLetters;
        }

        /// <summary>
        /// Возвращает слово из данной игровой сессии
        /// </summary>
        /// <param name="GameId">Идентификатор игровой сессии</param>
        /// <returns>Слово</returns>
        /// <exception cref="Exception">Выбрасывается если слово еще не загадали</exception>
        public string GetWord(int GameId)
        {
            var game = GetGame(GameId);

            return game.Word ?? throw new Exception("Слово еще не определено.");
        }

        /// <summary>
        /// Возвращает допустимое количество ошибок
        /// </summary>
        /// <param name="GameId">Идентификатор данной игровой сессии</param>
        /// <returns>Число допустимых ошибок</returns>
        public int GetMistakes(int GameId)
        {
            var game = GetGame(GameId);

            #pragma warning disable CS8629 // Тип значения, допускающего NULL, может быть NULL.
            return (int)game.Mistakes;
            #pragma warning restore CS8629 // Тип значения, допускающего NULL, может быть NULL.
        }
    }
}
