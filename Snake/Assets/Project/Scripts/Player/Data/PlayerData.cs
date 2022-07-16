using System;

namespace Snake.Player
{
    [Serializable]
    public class PlayerData
    {
        public string Nickname;
        public int Speed;
        public int Lenght;
        public int Score;

        public void SetData()
        {
            Nickname = PlayerRuntimeData.Nickname;
            Speed = PlayerRuntimeData.Speed;
            Lenght = PlayerRuntimeData.Lenght;
            Score = PlayerRuntimeData.Score;
        }
    }
}