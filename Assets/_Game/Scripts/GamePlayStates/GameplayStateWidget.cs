using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GameplayStates
{
    public class GameplayStateWidget : MonoBehaviour
    {
        [SerializeField]
        private Image _backgroundActive;
        [SerializeField]
        private Image _backgroundComplete;
        
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Image _checkmark;

        public void Initialize( Sprite icon )
        {
            _icon.sprite = icon;
            Refresh( false, false );
        }

        public void Refresh( bool isComplete, bool isActive )
        {
            _backgroundComplete.enabled = isComplete;
            _backgroundActive.enabled   = isActive;
            _checkmark.enabled          = isComplete;
        }
    }
}
