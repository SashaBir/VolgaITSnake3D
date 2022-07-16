using UnityEngine;

namespace Snake.Bonus
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class SpeedImprovement : Bonus
    {
        [SerializeField] [Min(0)] private int _count;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out ISpeedImprovable improvable) == false)
                return;

            improvable.SpeedImprove(_count);
            Destroy(gameObject);
        }
    }
}