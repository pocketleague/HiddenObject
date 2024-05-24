using UnityEngine;

namespace Scripts.UIAnimations
{
    public class UIAnimationSwing : MonoBehaviour
    {
        private Transform _transformCached;

        public float swingMagnitude = 15f;
        public float swingFrequency = 5f;
        
        private void Awake()
        {
            _transformCached = transform;
        }

        private void Update()
        {
            _transformCached.localRotation = Quaternion.Euler( 0f, 0f, Mathf.Sin( Time.time * swingFrequency ) * swingMagnitude );
        }
    }
}
