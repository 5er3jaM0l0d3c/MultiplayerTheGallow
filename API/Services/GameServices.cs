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
        /// <param name="MakerId">Идентификатор игрока разгадываюего слово</param>
        public int AddGame(int MakerId)
        {
            var game = new Game();
            game.MakerId = MakerId;
            context.Add(game);
            context.SaveChanges();
            var games = context.Games.ToList();
            games.Reverse();
            return games.FirstOrDefault(x => x.MakerId == MakerId).Id;
        }

        public void DeleteGame(int id)
        {
            var game = context.Games.FirstOrDefault(x => x.Id == id);
            try 
            {
                context.Remove(game);
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("Ошибка удаления игровой сессии.");
            }
        }

        public Game GetGame(int id)
        {
            return context.Games.Include(x => x.Destroyer).Include(x => x.Maker).FirstOrDefault(x => x.Id == id) ?? throw new Exception("Данной игровой сессии не существует.");
        }

        public List<Game> GetGames()
        {
            return context.Games.Include(x => x.Destroyer).Include(x => x.Maker).ToList();
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
            game.MistakesNum = numOfMistakes;
            context.Games.Update(game);
            context.SaveChanges();
        }
        /// <summary>
        /// Подключение к экземпляру игровой сессии игрока загадывающего слово
        /// </summary>
        /// <param name="MakerId">Идентификатор игрока</param>
        /// <param name="GameId">Идентификатор игры (необязательный - добавляет в первую свободную игровую сессию)</param>
        /// <returns>true - игра найдена</returns>
        /// <returns>false - поиск игры</returns>
        /// <exception cref="Exception">Выбрасывается если игровой сессии не существует или она занята</exception>
        public int? ConnectDestroyer(int DestroyerId, int GameId = -1)
        {  
            if (GameId == -1)
            {
                var game = context.Games.Include(x => x.Maker).Include(x => x.Destroyer).FirstOrDefault(x => x.DestroyerId == null);
                if(game != null)
                {
                    game.DestroyerId = DestroyerId;
                    game.Destroyer = context.Players.FirstOrDefault(x => x.Id == DestroyerId);
                    context.Games.Update(game);
                    context.SaveChanges();
                    return game.Id;
                }
                return null;
            }
            else
            {
                var game = GetGame(GameId);
                if(game.DestroyerId == null)
                {
                    game.DestroyerId = DestroyerId;
                    return game.Id;
                }
                else
                {
                    throw new Exception("Данной игровой сессии не существует или она уже занята.");
                }
            }
        }

        /// <summary>
        /// Проверяет присоединился ли игрок загадывающий слово к игровой сессии
        /// </summary>
        /// <param name="GameId">Идентификатор игры</param>
        public bool IsDestroyerConnected(int GameId)
        {
            var game = GetGame(GameId);

            if (game.DestroyerId != null)
                return true;
            else 
                return false;
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
            game.LastLetter = Letter;
            context.Update(game);
            if (game.Word.Any(c => c == Letter.First()))
            {
                var trueLetter = new TrueLetter();
                trueLetter.Letter = Letter;
                trueLetter.GameId = GameId;
                context.TrueLetters.Add(trueLetter);
                context.SaveChanges();
                return true;
            }

            game.MistakesNum--;
            context.Update(game);
            context.SaveChanges();
            return false;
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
            return (int)game.MistakesNum;
            #pragma warning restore CS8629 // Тип значения, допускающего NULL, может быть NULL.
        }

        public bool IsGameSetted(int GameId)
        {
            var game = GetGame(GameId);
            if(game.Word != null)
            {
                return true;
            }
            else
            { return false; }
        }

        public string? GetLastLetter(int GameId)
        {
            var game = GetGame(GameId);
            return game.LastLetter;
        }
    }
}
