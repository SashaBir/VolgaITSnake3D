using System;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.Ui.Game
{
    [Serializable]
    public class UiScore
    {
        [SerializeField] private Text _text;

        private int _score;

        public void Add()
        {
            _score++;
            _text.text = $"Очки: {_score}";
        }
    }
}