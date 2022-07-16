using System.Collections;
using UnityEngine;

namespace Snake.Player
{
    public class AnimationScale : MonoBehaviour
    {
        [SerializeField] private Vector3 _initial;
        [SerializeField] private Vector3 _final;
        [SerializeField] [Min(0)] private float _duration;

        private void OnEnable()
        {
            UpScale();
        }

        private void UpScale()
        {
            StartCoroutine(TransformScale(_initial, _final));
        }

        private IEnumerator TransformScale(Vector3 initial, Vector3 final)
        {
            float expanded = 0;

            do
            {
                var lerpRatio = expanded / _duration;
                transform.localScale = Vector3.Lerp(initial, final, lerpRatio);
                expanded += Time.deltaTime;

                yield return null;
            } while (_duration > expanded);
        }
    }
}