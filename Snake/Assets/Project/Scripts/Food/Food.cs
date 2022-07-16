using Snake.Spawner;
using UnityEngine;

namespace Snake.Food

{
    [RequireComponent(typeof(Collider))]
    public class Food : MonoBehaviour
    {
        [field: SerializeField] public int Saturation { get; private set; }

        public ISpawnerFood SpawnerFood { private get; set; }

        private void OnDisable()
        {
            SpawnerFood.SpawnFood();
        }
    }
}