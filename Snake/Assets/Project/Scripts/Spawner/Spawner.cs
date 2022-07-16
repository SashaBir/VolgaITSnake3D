using UnityEngine;

namespace Snake.Spawner
{
    public class Spawner : MonoBehaviour, ISpawnerFood, ISpawnerBonus
    {
        [SerializeField] private Spawnpoint[] _spawnpoints;
        [SerializeField] private Bonus.Bonus[] _bonuses;
        [SerializeField] private Food.Food[] _foods;

        private void Start()
        {
            SpawnFood();
            SpawnBonus();
        }

        public void SpawnBonus()
        {
            var index = Random.Range(0, _bonuses.Length);
            var spawnpoint = GetRandomSpawnpoint();
            Bonus.Bonus bonus = Instantiate(_bonuses[index], spawnpoint.transform.position, Quaternion.identity,spawnpoint.transform);
            bonus.SpawnerBonus = this;
        }

        public void SpawnFood()
        {
            var index = Random.Range(0, _foods.Length);
            var spawnpoint = GetRandomSpawnpoint();
            Food.Food food = Instantiate(_foods[index], spawnpoint.transform.position, Quaternion.identity, spawnpoint.transform);
            food.SpawnerFood = this;
        }

        private Spawnpoint GetRandomSpawnpoint()
        {
            Spawnpoint spawnpoint = null;

            do
            {
                var index = Random.Range(0, _spawnpoints.Length);
                spawnpoint = _spawnpoints[index];
            } 
            while (spawnpoint.IsFree == false);

            return spawnpoint;
        }
    }
}