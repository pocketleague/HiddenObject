using UnityEngine;

namespace Scripts.UI
{
    public class WindowPopUpAnimation : MonoBehaviour
    {
        private Transform _transformCached;
        private Window    _parentWindow;

        private Vector3 _startingLocalPosition;

        private       bool  _animating            = false;
        private       bool  _showing              = false;
        private       float _animationTimer       = 0f;
        private const float _animationSpeed       = 10f;
        private const float _positionDisplacement = 30;

        private void Awake()
        {
            _parentWindow          = GetComponentInParent<Window>();
            _transformCached       = transform;
            _startingLocalPosition = _transformCached.localPosition;

            _parentWindow.Opened += OnWindowOpened;
            _parentWindow.Closed += OnWindowClosed;
        }

        private void OnDestroy()
        {
            _parentWindow.Opened -= OnWindowOpened;
            _parentWindow.Closed -= OnWindowClosed;
        }

        private void Update()
        {
            HandleInOutAnimation();
        }

        private void HandleInOutAnimation()
        {
            if ( !_animating )
                return;

            _animationTimer                = Mathf.Clamp01( _animationTimer                                      + Time.deltaTime * _animationSpeed * ( _showing ? 1f : -1f ) );
            _transformCached.localPosition = _startingLocalPosition + Vector3.down * _positionDisplacement * ( 1 - _animationTimer );
        
            if ( _showing && _animationTimer == 1f || !_showing && _animationTimer == 0f )
                _animating = false;
        }

        private void OnWindowOpened()
        {
            _animating      = true;
            _showing        = true;
            _animationTimer = 0f;
        }

        private void OnWindowClosed()
        {
            _animating      = true;
            _showing        = false;
            _animationTimer = 1f;
        }
    }
}
