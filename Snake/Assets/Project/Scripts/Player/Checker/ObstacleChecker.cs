using System;
using System.Collections.Generic;
using Assets.Project.Physics;
using Snake.Audio;
using Snake.Surface;
using UnityEngine;

namespace Snake.Player
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class ObstacleChecker : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _initialTail;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Tail tail))
            {
                if (tail.Index > _initialTail)
                {
                    AudioReproducer.Instance.PlayBittedEffect();
                    Crash();
                }
            }

            if (other.GetComponent<Wall>() is not null)
            {
                AudioReproducer.Instance.PlayBittedEffect();
                Crash();
            }
        }

        public event Action OnObstacleBitten = delegate { };

        public void ChangeColliderPosition(Vector3 direction)
        {
            IDictionary<Vector3, Action> dictionary = new Dictionary<Vector3, Action>
            {
                {
                    Vector3.up,
                    () => PlaceCollider(new Vector3(0, 0.55f, 0), new Vector3(1, 0.1f, 1))
                },
                {
                    Vector3.down,
                    () => PlaceCollider(new Vector3(0, -0.55f, 0), new Vector3(1, 0.1f, 1))
                },
                {
                    Vector3.forward,
                    () => PlaceCollider(new Vector3(0, 0, 0.55f), new Vector3(1, 1, 0.1f))
                },
                {
                    Vector3.back,
                    () => PlaceCollider(new Vector3(0, 0, -0.55f), new Vector3(1, 1, 0.1f))
                },
                {
                    Vector3.left,
                    () => PlaceCollider(new Vector3(-0.55f, 0, 0), new Vector3(0.1f, 1, 1))
                },
                {
                    Vector3.right,
                    () => PlaceCollider(new Vector3(0.55f, 0, 0), new Vector3(-0.1f, 1, 1))
                },
                {
                    Vector3.zero,
                    () => { }
                }
            };

            dictionary[direction].Invoke();
        }

        private void PlaceCollider(Vector3 position, Vector3 scale)
        {
            transform.localPosition = position;
            transform.localScale = scale;
        }

        private void Crash()
        {
            OnObstacleBitten.Invoke();
            Debug.Log("Bitten!");
        }
    }
}