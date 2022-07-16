using System;
using System.Collections.Generic;
using System.Linq;
using Snake.Data;
using Snake.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.Ui.Menu
{
    [Serializable]
    public class UiTop
    {
        [SerializeField] private string _fileNameData;
        [SerializeField] private Text _text;
        [SerializeField] private Transform _container;

        public void ViewTop()
        {
            PlayerRuntimeData.FileNameData = _fileNameData;
            CollectionPlayersData data = CollectionPlayersData.GetCollection(_fileNameData);
            if (data.IsEmpty == true)
                return;

            Debug.Log("Ui Top");
            Spawn(data.Players);
        }

        private void Spawn(IEnumerable<PlayerData> data)
        {
            IOrderedEnumerable<PlayerData> sorted = data.OrderBy(i => i.Score >= 0);
            foreach (var player in sorted)
            {
                Text text = UnityEngine.Object.Instantiate(_text, _container);
                SetText(text, player.Nickname, player.Score);
            }
        }

        private void SetText(Text text, string nickname, int score)
        {
            text.text = $"{ nickname }: { score }";
        }
    }
}