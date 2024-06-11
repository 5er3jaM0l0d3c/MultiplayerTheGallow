namespace API
{
    public class Player
    {
        public enum Roles
        {
            Maker,
            Destroyer
        }

        public Roles? Role;
        public bool IsReady = false;
        public Player(Roles role)
        {
            Role = role;
            IsReady = role == Roles.Destroyer;
        }

    }
}
