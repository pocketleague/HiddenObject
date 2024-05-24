using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Unlockables
{
    [RequireComponent(typeof(Button))]
    public class UnlockableTabButtonView : MonoBehaviour
    {
        public event Action<EUnlockableType> OnClicked = delegate{ };

        public EUnlockableType type;

        private Button _buttonCached;

        [SerializeField]
        private CanvasGroup _activeGroup;
        [SerializeField]
        private CanvasGroup _inactiveGroup;

        private void Awake()
        {
            _buttonCached = GetComponent<Button>( );
            
            _buttonCached.onClick.AddListener( DispatchClickedEvent );
        }

        private void DispatchClickedEvent()
        {
            OnClicked.Invoke( type );
        }

        public void SetState( bool isActive )
        {
            _activeGroup.alpha          = isActive ? 1f : 0f;
            _activeGroup.interactable   = isActive;
            _activeGroup.blocksRaycasts = isActive;

            _inactiveGroup.alpha          = isActive ? 0f : 1f;
            _inactiveGroup.interactable   = !isActive;
            _inactiveGroup.blocksRaycasts = !isActive;
        }
    }
}
