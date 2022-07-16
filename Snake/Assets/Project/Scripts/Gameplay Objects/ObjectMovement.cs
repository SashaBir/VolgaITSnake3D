using System.Collections;
using UnityEngine;

namespace Snake.GameplayObjects
{
    public class ObjectMovement : MonoBehaviour
    {
        [SerializeField] private Transform _initial;
        [SerializeField] private Transform _final;
        [SerializeField] [Min(0)] private float _duration;

        private void Awake()
        {
            StartCoroutine(Repeat());
        }

        private void PutForward()
        {
            StartCoroutine(Move(_initial.position, _final.position));
        }

        private void Push()
        {
            StartCoroutine(Move(_final.position, _initial.position));
        }

        private IEnumerator Repeat()
        {
            while (true)
            {
                PutForward();
                yield return new WaitForSeconds(_duration);

                Push();
                yield return new WaitForSeconds(_duration);
            }
        }

        private IEnumerator Move(Vector3 from, Vector3 to)
        {
            float expanded = 0;

            do
            {
                var lerpRatio = expanded / _duration;
                transform.position = Vector3.Lerp(from, to, lerpRatio);
                expanded += Time.deltaTime;

                yield return null;
            } while (_duration > expanded);
        }
    }
}