using UnityEngine;

namespace Scripts.UI
{
    public class RotateUIView : MonoBehaviour
    {
        private Transform _transformCached;

        private const float _rotateSpeed = 120f;
        
        private void Awake()
        {
            _transformCached = transform;
        }

        private void Update()
        {
            _transformCached.Rotate( Vector3.forward * _rotateSpeed * Time.deltaTime );
        }
    }
}
