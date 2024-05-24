using System.Collections.Generic;
using Scripts.PlayerControl;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    [DefaultExecutionOrder(-1)]
    [RequireComponent(typeof(Window))]
    public class QuitGameWindow : MonoBehaviour
    {
        private IPlayerControlService _playerControlService;
        
        [SerializeField]
        private Window _window;

        public List<Window> blockingPopUps;

        [Inject]
        private void Construct( IPlayerControlService playerControlService )
        {
            _playerControlService = playerControlService;

            _playerControlService.OnPhysicalButtonPressed += OnButtonPressed;
        }

        private void OnDestroy()
        {
            _playerControlService.OnPhysicalButtonPressed -= OnButtonPressed;
        }

        private void OnButtonPressed( KeyCode key )
        {
            if ( key != KeyCode.Escape )
                return;

            CheckAndShowWindow();
        }

        private void CheckAndShowWindow()
        {
            foreach ( var window in blockingPopUps ) {
                if ( window.IsOpened )
                    return;
            }

            if ( !_window.IsOpened )
                _window.Open();
            else
                _window.Close();
        }

        private void Reset()
        {
            _window = GetComponent<Window>();
        }
    }
}
