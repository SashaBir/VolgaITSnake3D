using System;
using UnityEngine;

namespace Snake.Camera
{
    [Serializable]
    public struct CameraTransformData
    {
        [field: SerializeField] public Vector3 LocalPosition;
        [field: SerializeField] public Vector3 EulerAngles;
    }
}