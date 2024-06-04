using System;
using UnityEngine;

namespace Scripts.Camera
{
    public class CameraView : MonoBehaviour
    {
        public static Action<CameraView> CameraSpawned = delegate{ };

        public Transform positionTransform;
        public Transform rotationTransform;
        public Transform distanceTransform;

        public UnityEngine.Camera cameraObject;
        public GameObject completedParticle;

        private void Start()
        {
            CameraSpawned.Invoke( this );
        }
    }
}
