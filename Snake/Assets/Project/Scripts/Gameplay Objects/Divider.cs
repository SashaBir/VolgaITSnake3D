using Assets.Project.Physics;
using UnityEngine;

namespace Snake.GameplayObjects
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Divider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDividable dividable) == false)
                return;

            dividable.Divide();
        }
    }
}