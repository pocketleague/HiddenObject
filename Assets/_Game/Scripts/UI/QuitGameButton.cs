using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof( Button ) )]
    public class QuitGameButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener( OnButtonClicked );
        }

        void OnDestroy()
        {
            _button.onClick.RemoveListener( OnButtonClicked );
        }

        private void OnButtonClicked()
        {
            Application.Quit();
        }
    }
}
