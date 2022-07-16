using System;
using System.Collections.Generic;
using Snake.Data;
using UnityEngine;

namespace Snake.Player
{
    [Serializable]
    public struct CollectionPlayersData
    {
        public List<PlayerData> Players;

        public bool IsEmpty => Players.Count == 0;

        public static CollectionPlayersData GetCollection(string fileName)
        {
            return JsonManipulation.Read<CollectionPlayersData>(fileName);
        }
        
        public void Add(PlayerData player)
        {
            var index = Players.FindIndex(i => i.Nickname == player.Nickname);
            if (index == -1)
            {
                Players.Add(player);
                return;
            }

            int score = Players[index].Score;
            if (player.Score > score)
            {
                Players[index].Score = player.Score;
            }
        }

        public void Save(string fileName)
        {
            Debug.Log("Save");
            JsonManipulation.Write(Players, fileName);
        }
    }
}