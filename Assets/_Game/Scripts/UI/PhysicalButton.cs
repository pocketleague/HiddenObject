using Scripts.PlayerControl;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI
{
    [RequireComponent(typeof( Button ) )]
    public class PhysicalButton : MonoBehaviour
    {
        private IPlayerControlService _playerControlService;
        
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Window _parentWindow;
        
        public KeyCode triggerKey = KeyCode.Escape;

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
            if ( !_button.interactable )
                return;
            if ( !_parentWindow.IsOpened )
                return;
            if ( triggerKey != key )
                return;

            _button.onClick.Invoke();
        }

        private void Reset()
        {
            _button       = GetComponent<Button>();
            _parentWindow = GetComponentInParent<Window>();
        }
    }
}
