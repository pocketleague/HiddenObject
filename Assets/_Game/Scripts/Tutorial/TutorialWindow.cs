using Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Tutorial
{
    [RequireComponent(typeof(Window))]
    public class TutorialWindow : MonoBehaviour
    {
        private Window _windowCached;

        [SerializeField]
        private TextMeshProUGUI _title;
        [SerializeField]
        private TextMeshProUGUI _body;
        [SerializeField]
        private Image _icon;

        [Inject]
        private void Construct( ITutorialService tutorialService )
        {
            _windowCached = GetComponent<Window>( );
            
            tutorialService.RegisterWindow( this );
        }

        public void Show( string title, string body, Sprite icon )
        {
            _title.SetText( title );
            _body.SetText( body );
            _icon.sprite = icon;
            
            _windowCached.Open( );
        }

        public void Close( )
        {
            _windowCached.Close( );
        }
    }
}
