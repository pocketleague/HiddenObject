using System.Collections.Generic;
using UnityEngine;

namespace Scripts.PlayerControl
{
    [CreateAssetMenu( menuName = ( "Configs/PlayerControlConfig" ), fileName = "PlayerControlConfig" )]
    public class PlayerControlConfig : ScriptableObject
    {
        public List<KeyCode> handledPhysicalButtons;

        [Header( "Joystick" )]
        public bool  useJoystick      = true;
        public float joystickSize     = 0.2f;
        public float joystickDeadZone = 0.01f;

        [Header( "Drag" )]
        public bool  useDrag       = true;
        public float dragAmplitude = 1f;

        [Header("Click")]
        public bool useClick = true;
    }
}