using System.Diagnostics.Metrics;

namespace API
{
    public class Services: Interface
    {
        //----------------------------------------------------------------------------------------
        public static Player? Maker { get; set; }
        public static Player? Destroyer { get; set; }
        public static bool IsSecretMade { get; set; } = false;
        public static int NumOfMistakes { get; set; }


        public async void AddWord(string word)
        {
            var path = @"C:\Users\Admin\Projects\TheGallow\API\Word.txt";
            await File.WriteAllTextAsync(path, word);
        }



        public List<int> CheckLetter(string letter)
        {
            var path = @"C:\Users\Admin\Projects\TheGallow\API\Word.txt";
            var word = File.ReadAllText(path).ToLower();
            List<int> result = new List<int>();
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == Convert.ToChar(letter))
                    result.Add(i);
            }
            return result;
        }

        public string GetWord()
        {
            var path = @"C:\Users\Admin\Projects\TheGallow\API\Word.txt";
            var word = File.ReadAllText(path).ToLower();
            return word;
        }
        

        public async void WriteLastLetter(char letter)
        {
            var path = @"C:\Users\Admin\Projects\TheGallow\API\LLetter.txt";
            var l = Convert.ToString(letter);
            await File.WriteAllTextAsync(path, l);
        }

        

        public static void ReloadAlll()
        {
            Services seervis = new();
            seervis.ReloadAll();
        }

        public async void CheckWord(string word)
        {
            var path = @"C:\Users\Admin\Projects\TheGallow\API\LLetter.txt";
            await File.WriteAllTextAsync(path, word);
        }

        

        public int GetNumOfMistakes()
        {
            return NumOfMistakes;
        }
        //----------------------------------------------------------------------------------------

        /// <summary>
        /// Сигнал о готовности какого-то игрка
        /// </summary>
        /// <param name="player">Игрок</param>
        public void CreatePlayer(Player.Roles role)
        {
            if (role == Player.Roles.Destroyer)
            {
                Destroyer = new Player(role);
            }
            else
            {
                Maker = new Player(role);
            }
        }

        /// <summary>
        /// Создает слово для отгадывания
        /// </summary>
        /// <param name="word">Загадываемое слово</param>
        public void MakeSecret(string word)
        {
            var path = @"C:\Users\Admin\Projects\TheGallow\API\Word.txt";
            File.WriteAllTextAsync(path, word);
            IsSecretMade = true;
        }

        /// <summary>
        /// Проверяет занята ли роль
        /// </summary>
        /// <param name="role">Роль</param>
        /// <returns></returns>
        public bool CanTakeRole(Player.Roles role)
        {
            if(role == Player.Roles.Maker)
            {
                if (Maker == null)
                    return true;
                else
                    return false;
            }
            else
            {
                if (Destroyer == null)
                    return true;
                else
                    return false;
            }
        }

        public int CheckConnection(int num)
        {
            return num;
        }

        public bool IsWaitingPlayer(Player.Roles role)
        {
            if (role == Player.Roles.Destroyer) {
                if(Maker != null)
                    return Maker.IsReady ? false : true;
                return true;
            }
            else
            {
                return Destroyer == null ? true : false;
            }
        }

        public void SetNumOfMistakes(int num)
        {
            NumOfMistakes = num;
            Maker.IsReady = true;
        }

        public string TakeLastLetter()
        {
            var path = @"C:\Users\Admin\Projects\TheGallow\API\LLetter.txt";
            var charr = File.ReadAllText(path).ToLower();
            return charr;
        }

        public async void ReloadAll()
        {
            Maker = null;
            Destroyer = null;
            NumOfMistakes = 0;

            var path = @"C:\Users\Admin\Projects\TheGallow\API\LLetter.txt";
            await File.WriteAllTextAsync(path, "*");

            path = @"C:\Users\Admin\Projects\TheGallow\API\Word.txt";
            await File.WriteAllTextAsync(path, "");
        }
    }


}
