using Scripts.UI;
using UnityEngine;

namespace Scripts.Unlockables
{
    [RequireComponent(typeof(Window))]
    public class UnlockableTabView : MonoBehaviour
    {
        public EUnlockableType type;
        
        private Window _windowCached;

        private void Awake()
        {
            _windowCached = GetComponent<Window>( );
        }

        public void SetState( bool isActive )
        {
            if ( isActive == _windowCached.IsOpened )
                return;
            
            if ( isActive )
            {
                _windowCached.Open( );
            } else
            {
                _windowCached.Close( );
            }
        }
    }
}
