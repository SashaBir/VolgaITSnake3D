using System;
using System.Collections.Generic;
using Snake.Surface;
using UnityEngine;

namespace Assets.Project.Physics
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class SurfaceSlider : MonoBehaviour
    {
        private Vector3 _last = Vector3.zero;
        private Vector3 _normal = Vector3.zero;

        private int _numberOfCollision;

        public Vector3 DirectionDependingNormal { get; private set; } = Vector3.zero;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.GetComponent<Surface>() is null)
                return;

            _numberOfCollision++;
            _normal = collision.contacts[0].normal;
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.transform.GetComponent<Surface>() is null)
                return;

            _numberOfCollision--;
        }

        public Vector3 Project(Vector3 direction)
        {
            // Vector3.left = -1, 0, 0
            // Vector3.right = 1, 0, 0
            // Vector3.forward = 0, 0, 1
            // Vector3.back = 0, 0, -1
            // Vector3.down = 0, -1, 0
            // Vector3.up = 0, 1, 0
            
            DirectionDependingNormal = direction;

            if (_numberOfCollision == 0)
            {
                IDictionary<Vector3, Func<Vector3, Vector3>> outside = new Dictionary<Vector3, Func<Vector3, Vector3>>
                {
                    { Vector3.up, GetOutsideDirectionUp },
                    { Vector3.down, GetOutsideDirectionDown },
                    { Vector3.forward, GetOutsideDirectionForward },
                    { Vector3.back, GetOutsideDirectionBack },
                    { Vector3.left, GetOutsideDirectionLeft },
                    { Vector3.right, GetOutsideDirectionRight },
                    { Vector3.zero, GetOutsideDirectionZero }
                };

                var resultOutside = outside[_normal].Invoke(direction);

                _last = _normal;

                return resultOutside;
            }

            IDictionary<Vector3, Func<Vector3, Vector3>> inside = new Dictionary<Vector3, Func<Vector3, Vector3>>
            {
                { Vector3.up, GetInsideDirectionUp },
                { Vector3.down, GetInsideDirectionDown },
                { Vector3.forward, GetInsideDirectionForward },
                { Vector3.back, GetInsideDirectionBack },
                { Vector3.left, GetInsideDirectionLeft },
                { Vector3.right, GetInsideDirectionRight },
                { Vector3.zero, GetInsideDirectionZero }
            };

            var resultInside = inside[_normal].Invoke(direction);

            _last = _normal;

            return resultInside;
        }

        // Inside
        private Vector3 GetInsideDirectionUp(Vector3 direction)
        {
            return direction;
        }

        private Vector3 GetInsideDirectionDown(Vector3 direction)
        {
            return -direction;
        }

        private Vector3 GetInsideDirectionForward(Vector3 direction)
        {
            if (_last == Vector3.right)
            {
                DirectionDependingNormal = Vector3.right;
                return Vector3.right;
            }

            if (_last == Vector3.left)
            {
                DirectionDependingNormal = Vector3.left;
                return Vector3.left;
            }

            //Debug.Log("forward");

            if (direction == Vector3.back) return Vector3.up;

            if (direction == Vector3.left) return Vector3.left;

            if (direction == Vector3.forward)
                return Vector3.down;
            return Vector3.right;
        }

        private Vector3 GetInsideDirectionBack(Vector3 direction)
        {
            if (_last == Vector3.right)
            {
                DirectionDependingNormal = Vector3.right;
                return Vector3.right;
            }

            if (_last == Vector3.left)
            {
                DirectionDependingNormal = Vector3.left;
                return Vector3.left;
            }

            //Debug.Log("back");

            if (direction == Vector3.forward) return Vector3.up;

            if (direction == Vector3.right) return Vector3.right;

            if (direction == Vector3.back)
                return Vector3.down;
            return Vector3.left;
        }

        private Vector3 GetInsideDirectionLeft(Vector3 direction)
        {
            if (_last == Vector3.forward)
            {
                DirectionDependingNormal = Vector3.forward;
                return Vector3.forward;
            }

            if (_last == Vector3.back)
            {
                DirectionDependingNormal = Vector3.back;
                return Vector3.back;
            }

            //Debug.Log("left");

            if (direction == Vector3.forward) return Vector3.forward;

            if (direction == Vector3.left) return Vector3.down;

            if (direction == Vector3.back)
                return Vector3.back;
            return Vector3.up;
        }

        private Vector3 GetInsideDirectionRight(Vector3 direction)
        {
            if (_last == Vector3.back)
            {
                DirectionDependingNormal = Vector3.back;
                return Vector3.back;
            }

            if (_last == Vector3.forward)
            {
                DirectionDependingNormal = Vector3.forward;
                return Vector3.forward;
            }

            //Debug.Log("right");

            if (direction == Vector3.left) return Vector3.up;

            if (direction == Vector3.forward) return Vector3.forward;

            if (direction == Vector3.right)
                return Vector3.down;
            return Vector3.back;
        }

        private Vector3 GetInsideDirectionZero(Vector3 _)
        {
            return Vector3.zero;
        }

        // Outside
        private Vector3 GetOutsideDirectionUp(Vector3 direction)
        {
            if (direction == Vector3.forward) _normal = Vector3.forward;

            if (direction == Vector3.back) _normal = Vector3.back;

            if (direction == Vector3.right) _normal = Vector3.right;

            if (direction == Vector3.left) _normal = Vector3.left;

            return Vector3.down;
        }

        private Vector3 GetOutsideDirectionDown(Vector3 direction)
        {
            if (direction == Vector3.forward) _normal = Vector3.back;

            if (direction == Vector3.back) _normal = Vector3.forward;

            if (direction == Vector3.right) _normal = Vector3.left;

            if (direction == Vector3.left) _normal = Vector3.right;

            return Vector3.up;
        }

        private Vector3 GetOutsideDirectionForward(Vector3 direction)
        {
            if (direction == Vector3.back) _normal = Vector3.up;

            if (direction == Vector3.forward) _normal = Vector3.down;

            if (direction == Vector3.left)
            {
                _normal = Vector3.left;
                DirectionDependingNormal = Vector3.back;
            }

            if (direction == Vector3.right)
            {
                _normal = Vector3.right;
                DirectionDependingNormal = Vector3.back;
            }

            return Vector3.back;
        }

        private Vector3 GetOutsideDirectionBack(Vector3 direction)
        {
            if (direction == Vector3.forward) _normal = Vector3.up;

            if (direction == Vector3.back) _normal = Vector3.down;

            if (direction == Vector3.left)
            {
                _normal = Vector3.left;
                DirectionDependingNormal = Vector3.forward;
            }

            if (direction == Vector3.right)
            {
                _normal = Vector3.right;
                DirectionDependingNormal = Vector3.forward;
            }

            return Vector3.forward;
        }

        private Vector3 GetOutsideDirectionLeft(Vector3 direction)
        {
            if (direction == Vector3.right) _normal = Vector3.up;

            if (direction == Vector3.left) _normal = Vector3.down;

            if (direction == Vector3.forward)
            {
                DirectionDependingNormal = Vector3.right;
                _normal = Vector3.forward;
            }

            if (direction == Vector3.back)
            {
                DirectionDependingNormal = Vector3.right;
                _normal = Vector3.back;
            }

            return Vector3.right;
        }

        private Vector3 GetOutsideDirectionRight(Vector3 direction)
        {
            if (direction == Vector3.left) _normal = Vector3.up;

            if (direction == Vector3.right) _normal = Vector3.down;

            if (direction == Vector3.back)
            {
                DirectionDependingNormal = Vector3.left;
                _normal = Vector3.back;
            }

            if (direction == Vector3.forward)
            {
                DirectionDependingNormal = Vector3.left;
                _normal = Vector3.forward;
            }

            return Vector3.left;
        }

        private Vector3 GetOutsideDirectionZero(Vector3 _)
        {
            return Vector3.zero;
        }
    }
}