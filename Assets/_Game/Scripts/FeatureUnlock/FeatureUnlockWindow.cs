using Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.FeatureUnlock
{
    [RequireComponent(typeof(Window))]
    public class FeatureUnlockWindow : MonoBehaviour
    {
        private Window _windowCached;

        [SerializeField]
        private TextMeshProUGUI _featureNameLabel;
        [SerializeField]
        private Image _featureImage;

        [Inject]
        private void Construct( IFeatureUnlockService service )
        {
            _windowCached = GetComponent<Window>( );
            
            service.RegisterWindow( this );
        }

        public void ShowFeatureUnlock( FeatureUnlockConfig featureConfig )
        {
            _featureNameLabel.SetText( featureConfig.featureName );
            _featureImage.sprite = featureConfig.featureSprite;

            _windowCached.Open( );
        }
    }
}
