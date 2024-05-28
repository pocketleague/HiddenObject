using System;
using System.Linq;
using Scripts.GameLoop;
using UnityEngine;
using Zenject;

namespace Scripts.PlayerControl
{
    public class PlayerControlService : IPlayerControlService
    {
        public event Action<KeyCode> OnPhysicalButtonPressed = delegate { };

        private PlayerControlConfig _config;
        
        public PlayerControlDragModule      DragModule     { get; private set; }
        public PlayerControlJoystickModule  JoystickModule { get; private set; }
        public PlayerControlClickModule     ClickModule     { get; private set; }

        public bool PlayerControlsEnabled { get; private set; }

        [Inject]
        private void Construct( PlayerControlConfig config, IPlayerLoop playerLoop )
        {
            _config               = config;
            PlayerControlsEnabled = true;

            DragModule     = new PlayerControlDragModule( config );
            JoystickModule = new PlayerControlJoystickModule( config );
            ClickModule = new PlayerControlClickModule(config);

            playerLoop.OnUpdateTick += CheckInput;
        }

        private void CheckInput()
        {
            if ( !PlayerControlsEnabled )
                return;
            
            HandlePhysicalButtons( );
            
            if ( _config.useJoystick )
                JoystickModule.Tick( );
            
            if ( _config.useDrag )
                DragModule.Tick( );

            if ( _config.useClick )
                ClickModule.Tick();
        }

        private void HandlePhysicalButtons()
        {
            foreach ( var physicalButton in _config.handledPhysicalButtons.Where( Input.GetKeyDown ) )
            {
                OnPhysicalButtonPressed.Invoke( physicalButton );
            }
        }
    }
}
