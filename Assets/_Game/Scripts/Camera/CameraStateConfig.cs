using UnityEngine;

namespace Scripts.Camera
{
    [CreateAssetMenu( menuName = ( "Configs/CameraStateConfig" ), fileName = "CameraStateConfig" )]
    public class CameraStateConfig : ScriptableObject
    {
        public string stateId;

        public Vector3 movementOffset;
        public float   movementChangeSpeed;
        [Space(10)]
        public float distance;
        public float distanceChangeSpeed;
        [Space(10)]
        public Vector3 rotation;
        public float rotationChangeSpeed;
        [Space(10)]
        public Vector2 clipping;
        public float clippingChangeSpeed;
        [Space(10)]
        public float fieldOfView;
        public float fieldOfViewChangeSpeed;
    }
}