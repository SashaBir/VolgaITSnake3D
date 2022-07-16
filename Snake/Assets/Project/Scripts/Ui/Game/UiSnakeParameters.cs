using System;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.Ui.Game
{
    [Serializable]
    public class UiSnakeParameters
    {
        [SerializeField] private Text _text;

        public void SetSpeed(int speed)
        {
            if (speed < 0)
                throw new Exception("The speed is less than zero.");

            _text.text = $"Скорость: {speed}";
        }
    }
}