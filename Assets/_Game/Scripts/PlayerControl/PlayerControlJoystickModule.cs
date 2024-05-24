using System;
using UnityEngine;

namespace Scripts.PlayerControl
{
    public class PlayerControlJoystickModule
    {
        public event Action<Vector3> OnJoystickStarted = delegate { };
        public event Action<Vector3> OnJoystick        = delegate { };
        public event Action<Vector3> OnJoystickEnded   = delegate { };

        private readonly PlayerControlConfig _config;
        
        private Vector3 _previousTouchPosition;

        public PlayerControlJoystickModule( PlayerControlConfig config )
        {
            _config = config;
        }

        public void Tick( )
        {
            if ( Input.GetMouseButtonDown( 0 ) )
            {
                OnJoystickStarted.Invoke( Vector3.zero );
                
                _previousTouchPosition = Input.mousePosition;
            }
            else if ( Input.GetMouseButton( 0 ) )
            {
                var joystickVector = ( Input.mousePosition - _previousTouchPosition ) / Screen.height;

                if ( joystickVector.sqrMagnitude >= _config.joystickSize * _config.joystickSize )
                    joystickVector = joystickVector.normalized * _config.joystickSize;
                
                if ( joystickVector.sqrMagnitude < _config.joystickDeadZone * _config.joystickDeadZone )
                    joystickVector = Vector3.zero;

                OnJoystick.Invoke( joystickVector / _config.joystickSize );
            }
            else if ( Input.GetMouseButtonUp( 0 ) )
            {
                OnJoystickEnded.Invoke( Vector3.zero );

                _previousTouchPosition = Input.mousePosition;
            }
        }
    }
}
