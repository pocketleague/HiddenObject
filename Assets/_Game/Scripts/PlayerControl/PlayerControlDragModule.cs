using System;
using UnityEngine;

namespace Scripts.PlayerControl
{
    public class PlayerControlDragModule
    {
        public event Action<Vector3> OnDragStarted = delegate { };
        public event Action<Vector3> OnDrag        = delegate { };
        public event Action<Vector3> OnDragEnded   = delegate { };

        private readonly PlayerControlConfig _config;
        
        private Vector3 _previousTouchPosition;

        public PlayerControlDragModule( PlayerControlConfig config )
        {
            _config = config;
        }

        public void Tick( )
        {
            if ( Input.GetMouseButtonDown( 0 ) )
            {
                OnDragStarted.Invoke( Vector3.zero );
                
                _previousTouchPosition = Input.mousePosition;
            }
            else if ( Input.GetMouseButton( 0 ) )
            {
                OnDrag.Invoke( ( Input.mousePosition - _previousTouchPosition ) * _config.dragAmplitude / Screen.height );
                
                _previousTouchPosition = Input.mousePosition;
            }
            else if ( Input.GetMouseButtonUp( 0 ) )
            {
                OnDragEnded.Invoke( Vector3.zero );

                _previousTouchPosition = Input.mousePosition;
            }
        }
    }
}
