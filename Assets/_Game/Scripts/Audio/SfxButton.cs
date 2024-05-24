using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Audio
{
    [RequireComponent(typeof(Button))]
    public class SfxButton : MonoBehaviour
    {
        public bool isPositive = true;
        public bool isNegative = false;
        public bool isPurchase = false;

        [SerializeField]
        private Button _buttonCached;

        [Inject]
        private ISfxService _sfxService;

        void Awake()
        {
            _buttonCached.onClick.AddListener( OnButtonClicked );
        }

        private void OnDestroy()
        {
            _buttonCached.onClick.RemoveListener( OnButtonClicked );
        }

        private void OnButtonClicked()
        {
            if ( isPositive )
                _sfxService.PlayUiTapPositive( );
            else if ( isNegative )
                _sfxService.PlayUiTapNegative( );
            else if ( isPurchase )
                _sfxService.PlayUiTapPurchase( );
        }

        private void Reset()
        {
            _buttonCached = GetComponent<Button>();
        }
    }
}
