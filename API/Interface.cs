namespace API
{
    public interface Interface
    {
        List<int> CheckLetter(string letter);
        string GetWord();
        void WriteLastLetter(char letter);
        void CheckWord(string word);

        //------------------------------------------------------------------------------

        int CheckConnection(int num);
        bool CanTakeRole(Player.Roles role);

        static Player? Maker { get; set; }
        static Player? Destroyer { get; set; }
        static bool IsSecretMade { get; set; }
        bool IsWaitingPlayer(Player.Roles role);
        void CreatePlayer(Player.Roles role);
        void MakeSecret(string word);
        string TakeLastLetter();
        int GetNumOfMistakes();
        void SetNumOfMistakes(int num);
        void ReloadAll();

    }
}
