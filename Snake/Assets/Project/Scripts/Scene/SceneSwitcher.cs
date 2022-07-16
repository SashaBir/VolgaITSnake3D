using System;
using Snake.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snake.Scene
{
    [Serializable]
    public class SceneSwitcher
    {
        [SerializeField] [Min(0)] private int _menu;
        [SerializeField] [Min(0)] private int _game;

        public void SwitchToMenu()
        {
            AudioReproducer.Instance.TurnOnEffect();
            AudioReproducer.Instance.TurnOnMusic();
            
            SceneManager.LoadScene(_menu);
        }

        public void SwitchToGame()
        {
            SceneManager.LoadScene(_game);
        }
    }
}