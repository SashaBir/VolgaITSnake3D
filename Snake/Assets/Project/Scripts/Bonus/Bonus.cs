using Snake.Spawner;
using UnityEngine;

namespace Snake.Bonus
{
    public class Bonus : MonoBehaviour
    {
        public ISpawnerBonus SpawnerBonus { private get; set; }

        private void OnDisable()
        {
            SpawnerBonus.SpawnBonus();
        }
    }
}