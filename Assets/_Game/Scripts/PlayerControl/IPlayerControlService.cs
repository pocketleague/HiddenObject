using System;
using UnityEngine;

namespace Scripts.PlayerControl
{
    public interface IPlayerControlService
    {
        event Action<KeyCode> OnPhysicalButtonPressed;
        
        PlayerControlDragModule     DragModule     { get; }
        PlayerControlJoystickModule JoystickModule { get; }
        
        bool PlayerControlsEnabled { get; }
    }
}
