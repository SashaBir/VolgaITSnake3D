using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Project.Physics;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.Ui.Game
{
    [Serializable]
    public class UiSnakeLenght
    {
        [SerializeField] private Text _text;

        public void Add(IEnumerable<Tail> tails)
        {
            _text.text = $"Длина: { tails.Count() }";
        }
    }
}