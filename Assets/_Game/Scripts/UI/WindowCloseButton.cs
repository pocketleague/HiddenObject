using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof( Button ) )]
    public class WindowCloseButton : MonoBehaviour
    {
        private Button _button;
        private Window _parentWindow;

        private void Awake()
        {
            _button       = GetComponent<Button>();
            _parentWindow = GetComponentInParent<Window>();

            _button.onClick.AddListener( OnButtonClicked );
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener( OnButtonClicked );
        }

        private void OnButtonClicked()
        {
            if ( _parentWindow == null || !_parentWindow.IsOpened )
                return;

            _parentWindow.Close();
        }
    }
}
