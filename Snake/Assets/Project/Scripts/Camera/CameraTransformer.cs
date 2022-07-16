using UnityEngine;

namespace Snake.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraTransformer : MonoBehaviour
    {
        public Transform a;

        [Header("Position")] 
        [SerializeField] private CameraTransformData _up;
        [SerializeField] private CameraTransformData _down;

        [Header("Field of view")] 
        [SerializeField] private float _minimumView;

        [SerializeField] private float _maximumView;
        [SerializeField] [Min(0)] private float _stepView;

        private UnityEngine.Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
        }

        public void LookAtUp()
        {
            SetTransform(_up);
        }

        public void LookAtDown()
        {
            SetTransform(_down);
        }

        public void ZoomIn()
        {
            if (_camera.fieldOfView + _stepView <= _maximumView)
                _camera.fieldOfView += _stepView;
        }

        public void ZoomOut()
        {
            if (_camera.fieldOfView - _stepView >= _minimumView)
                _camera.fieldOfView -= _stepView;
        }

        private void SetTransform(CameraTransformData data)
        {
            transform.localPosition = data.LocalPosition;
            transform.eulerAngles = data.EulerAngles;
        }
    }
}