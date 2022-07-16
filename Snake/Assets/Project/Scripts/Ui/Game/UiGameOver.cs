using System;
using Snake.Data;
using Snake.Player;
using Snake.Scene;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.Ui.Game
{
    [Serializable]
    public class UiGameOver
    {
        [Header("Panel")] 
        [SerializeField] private GameObject _gameover;
        [SerializeField] private GameObject _parametrs;
        
        [Header("Buttons")] 
        [SerializeField] private Button _restarted;
        [SerializeField] private Button _menu;

        [Header("Texts")] 
        [SerializeField] private Text _nickname;
        [SerializeField] private Text _speed;
        [SerializeField] private Text _lenght;
        [SerializeField] private Text _score;
        
        [SerializeField] private SceneSwitcher _switcher;

        public void Initialize()
        {
            _restarted.onClick.AddListener(_switcher.SwitchToGame);
            _menu.onClick.AddListener(_switcher.SwitchToMenu);
        }

        public void ShowGameOver()
        {
            _parametrs.SetActive(false);
            _gameover.SetActive(true);

            UpdateText();
        }

        private void UpdateText()
        {
            _nickname.text = $"Имя: { PlayerRuntimeData.Nickname } ";
            _speed.text = $"Скорость: { PlayerRuntimeData.Speed.ToString() } ";
            _lenght.text = $"Длина: { PlayerRuntimeData.Lenght.ToString() } ";
            _score.text = $"Очки: { PlayerRuntimeData.Score.ToString() } ";
        }
    }
}