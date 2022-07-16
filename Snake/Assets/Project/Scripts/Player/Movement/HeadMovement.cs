using System;
using Snake.Audio;
using Snake.Bonus;
using Snake.Player;
using UnityEngine;

namespace Assets.Project.Physics
{
    public class HeadMovement : MonoBehaviour, IMovableParamenters, ISpeedImprovable, ISpeedDeclinable
    {
        [SerializeField] private SurfaceSlider _surfaceSlider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Vector3 _direction;
        [SerializeField] [Min(1)] private int _speed;

        public event Action<Vector3> OnMoved = delegate { };
        public event Action<Vector3> OnTurned = delegate { };

        private Vector3 _directionAlongSurface = Vector3.zero;
        private Vector3 _lastDirectionAlongSurface = Vector3.one;

        private void FixedUpdate()
        {
            Move();
        }

        public int Speed => _speed;

        public void SpeedImprove(int count)
        {
            if (count <= 0)
                throw new Exception("Count is less then zero.");

            _speed += count;

            PlayerRuntimeData.Speed = _speed;
        }

        public void SpeedWorsen(int count)
        {
            if (count <= 0)
                throw new Exception("Count is less then zero.");

            _speed -= count;
            if (_speed <= 0)
                _speed = 1;
            
            PlayerRuntimeData.Speed = _speed;
        }

        public void MoveLeft()
        {
            if (_direction == Vector3.forward)
                _direction = Vector3.left;
            else if (_direction == Vector3.left)
                _direction = Vector3.back;
            else if (_direction == Vector3.back)
                _direction = Vector3.right;
            else if (_direction == Vector3.right)
                _direction = Vector3.forward;
            
            AudioReproducer.Instance.PlayTurnedEffect();
        }

        public void MoveRight()
        {
            if (_direction == Vector3.forward)
                _direction = Vector3.right;
            else if (_direction == Vector3.right)
                _direction = Vector3.back;
            else if (_direction == Vector3.back)
                _direction = Vector3.left;
            else if (_direction == Vector3.left)
                _direction = Vector3.forward;
            
            AudioReproducer.Instance.PlayTurnedEffect();
        }

        public void Stop()
        {
            _speed = 0;
        }

        private void Move()
        {
            if (_lastDirectionAlongSurface != _directionAlongSurface)
            {
                AudioReproducer.Instance.PlayTurnedEffect();
                _lastDirectionAlongSurface = _directionAlongSurface;
            }
            
            _directionAlongSurface = _surfaceSlider.Project(_direction);
            var offset = _directionAlongSurface * (_speed * Time.fixedDeltaTime);

            _rigidbody.MovePosition(_rigidbody.position + offset);

            OnMoved.Invoke(_rigidbody.position);
            OnTurned.Invoke(_directionAlongSurface);

            DirectionDependingNormal();
        }

        private void DirectionDependingNormal()
        {
            _direction = _surfaceSlider.DirectionDependingNormal;
        }
    }
}