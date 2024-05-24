using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Unlockables
{
    [RequireComponent(typeof(Button))]
    public class UnlockableWidgetView : MonoBehaviour
    {
        public event Action<UnlockableConfig> OnItemClicked = delegate{ };

        private Button _buttonCached;
        
        public UnlockableConfig item;

        [SerializeField]
        private Image _uiImage;

        [SerializeField]
        private CanvasGroup _lockedState;
        [SerializeField]
        private CanvasGroup _unlockedState;
        [SerializeField]
        private CanvasGroup _selectedState;

        private void Awake()
        {
            _buttonCached = GetComponent<Button>( );
            
            _buttonCached.onClick.AddListener( DispatchClickedEvent );
        }

        private void OnDestroy()
        {
            _buttonCached.onClick.RemoveListener( DispatchClickedEvent );
        }

        public void Initialize( UnlockableConfig config )
        {
            item = config;
            
            _uiImage.sprite = item.storeImage;
        }

        public void RefreshState( bool unlocked, bool selected )
        {
            _lockedState.alpha   = !unlocked && !selected ? 1f : 0f;
            _unlockedState.alpha = unlocked  && !selected ? 1f : 0f;
            _selectedState.alpha = unlocked  && selected  ? 1f : 0f;
        }

        private void DispatchClickedEvent()
        {
            OnItemClicked.Invoke( item );
        }
    }
}
