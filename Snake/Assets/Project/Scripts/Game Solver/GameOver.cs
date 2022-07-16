using Snake.Data;
using Snake.Player;
using Snake.Ui.Game;
using UnityEngine;

namespace Snake.GameSolver
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;
        [SerializeField] private UiGameOver _ui;

        private void Awake()
        {
            _ui.Initialize();
        }

        private void OnEnable()
        {
            _player.OnDied += Finish;
        }

        private void OnDisable()
        {
            _player.OnDied -= Finish;
        }

        private void Finish()
        {
            _ui.ShowGameOver();
            SavePlayerData();
        }

        private void SavePlayerData()
        {
            PlayerData playerData = new()
            {
                Nickname = PlayerRuntimeData.Nickname,
                Speed = PlayerRuntimeData.Speed,
                Lenght = PlayerRuntimeData.Lenght,
                Score = PlayerRuntimeData.Score
            };

            string fileName = PlayerRuntimeData.FileNameData;
            
            CollectionPlayersData data = CollectionPlayersData.GetCollection(fileName);
            data.Add(playerData);
            data.Save(fileName);
        }
    }
}