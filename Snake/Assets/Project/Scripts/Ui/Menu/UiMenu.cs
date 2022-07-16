using System;
using Snake.Audio;
using Snake.Player;
using Snake.Scene;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.Ui.Menu
{
    public class UiMenu : MonoBehaviour
    {
        [Header("Ui Elements")] 
        [SerializeField] private InputField _nickname;
        [SerializeField] private Button _started;
        [SerializeField] private Button _exited;
        [SerializeField] private Toggle _music;
        [SerializeField] private Toggle _sound;

        [Header("Top")] 
        [SerializeField] private UiTop _top;
        
        [Header("Scene")] 
        [SerializeField] private SceneSwitcher _switcher;
        
        private const string DEFAULT_NICKNAME = "Player";
        
        private void Awake()
        {
            _started.onClick.AddListener(StartGame);
            _exited.onClick.AddListener(Exit);
            _music.onValueChanged.AddListener(isOn =>
            {
                Action method = isOn ? AudioReproducer.Instance.TurnOnMusic : AudioReproducer.Instance.TurnOffMusic;
                method.Invoke();
            });
            _sound.onValueChanged.AddListener(isOn =>
            {
                Action method = isOn ? AudioReproducer.Instance.TurnOnEffect : AudioReproducer.Instance.TurnOffEffect;
                method.Invoke();
            });
            
            PlayerRuntimeData.Clear();
            _top.ViewTop();
        }

        private void StartGame()
        {
            bool isValidated = ValidateNickname(_nickname.text);
            if (isValidated == false)
            {
                _nickname.text = DEFAULT_NICKNAME;
                PlayerRuntimeData.Nickname = DEFAULT_NICKNAME;
                return;
            }
            
            PlayerRuntimeData.Nickname = _nickname.text;
            
            _switcher.SwitchToGame();
        }

        private void Exit()
        {
            Application.Quit();
        }

        private bool ValidateNickname(string nickname) =>
            string.IsNullOrEmpty(nickname) == false && string.IsNullOrWhiteSpace(nickname) == false;
    }
}