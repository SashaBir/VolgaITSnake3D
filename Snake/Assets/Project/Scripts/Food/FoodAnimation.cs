using UnityEngine;

namespace Snake.Food
{
    public class FoodAnimation : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float _speed;

        private float _angle;

        private void Awake()
        {
            _angle = Random.Range(0, 2) == 0 ? _speed : -_speed;
        }

        private void Update()
        {
            transform.RotateAround(transform.position, Vector3.up, _angle * Time.smoothDeltaTime);
        }
    }
}