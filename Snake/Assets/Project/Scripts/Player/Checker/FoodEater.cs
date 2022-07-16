using System;
using Snake.Audio;
using UnityEngine;

namespace Snake.Player
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class FoodEater : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Food.Food food) == false)
                return;

            Eat(food);
        }

        public event Action OnEaten = delegate { };

        private void Eat(Food.Food food)
        {
            for (var i = 0; i < food.Saturation; i++)
                OnEaten.Invoke();

            Destroy(food.gameObject);
            
            PlayerRuntimeData.Score++;
            
            AudioReproducer.Instance.PlayAppleEffect();
        }
    }
}