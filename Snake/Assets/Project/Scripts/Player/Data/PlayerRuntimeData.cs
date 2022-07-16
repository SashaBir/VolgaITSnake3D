namespace Snake.Player
{
    public static class PlayerRuntimeData
    {
        public static string Nickname;
        public static int Speed;
        public static int Lenght;
        public static int Score;

        public static string FileNameData;

        public static void Clear()
        {
            Nickname = default;
            Speed = 0;
            Lenght = 0;
            Score = 0;
        }
    }
}