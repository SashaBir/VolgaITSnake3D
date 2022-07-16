using UnityEngine;

namespace Snake.Spawner
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Spawnpoint : MonoBehaviour
    {
        private int _numberCollision = 0;

        public bool IsFree => _numberCollision == 0;

        private void OnCollisionEnter(Collision collision)
        {
            _numberCollision++;
        }

        private void OnCollisionExit(Collision other)
        {
            _numberCollision--;
        }

        private void OnTriggerEnter(Collider other)
        {
            _numberCollision++;
        }

        private void OnTriggerExit(Collider other)
        {
            _numberCollision--;
        }
    }
}