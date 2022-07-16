using UnityEngine;

namespace Snake.Bonus
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class SpeedDecline : Bonus
    {
        [SerializeField] [Min(0)] private int _count;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out ISpeedDeclinable declinable) == false)
                return;

            declinable.SpeedWorsen(_count);
            Destroy(gameObject);
        }
    }
}