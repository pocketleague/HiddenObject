using Scripts.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.Tutorial
{
    [RequireComponent( typeof(Window))]
    public class TutorialPopUp : MonoBehaviour
    {
        private Window _windowCached;

        [SerializeField]
        private TextMeshProUGUI _label;
        
        [Inject]
        private void Construct( ITutorialService tutorialService )
        {
            _windowCached = GetComponent<Window>( );
            
            tutorialService.RegisterPopUp( this );
        }

        public void Show( string text )
        {
            _label.SetText( text );
            
            _windowCached.Open( );
        }

        public void Close( )
        {
            _windowCached.Close( );
        }
    }
}
