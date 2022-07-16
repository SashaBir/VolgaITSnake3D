using System.Collections;
using UnityEngine;

namespace Assets.Project.Physics
{
    [RequireComponent(typeof(Collider))]
    public class Tail : MonoBehaviour, IDividable
    {
        public ICropper Cropper { private get; set; }

        public Vector3 DirectionAsOffset { get; private set; }

        public int Index { get; set; }

        public void Divide()
        {
            Cropper.Crop(this);
        }

        public void MoveTo(Vector3 front, float speed)
        {
            StartCoroutine(Move(front, speed));
        }

        private IEnumerator Move(Vector3 target, float speed)
        {
            var from = transform.position;

            var duration = 1f / speed;
            var expanded = 0f;

            do
            {
                var lerpRatio = expanded / duration;
                var position = Vector3.Lerp(from, target, lerpRatio);
                transform.position = position;
                expanded += Time.smoothDeltaTime;

                DirectionAsOffset = position.normalized;

                yield return null;
            } while (duration > expanded);
        }
    }
}