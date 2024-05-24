using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Audio
{
    [RequireComponent(typeof( Button ) )]
    public class SfxEnabledToggle : MonoBehaviour
    {
        private ISfxService _sfxService;
        
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Image  _buttonImage;
        
        public  Image  image;

        public Sprite activeSprite;
        public Sprite inactiveSprite;

        public Sprite activeButtonSprite;
        public Sprite inactiveButtonSprite;

        [Inject]
        private void Construct( ISfxService sfxService )
        {
            _sfxService = sfxService;

            _button.onClick.AddListener( OnButtonClicked );

            _sfxService.OnSfxEnabledChanged += RefreshSprite;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener( OnButtonClicked );
        }

        private void OnButtonClicked()
        {
            _sfxService.ToggleSfxEnabled( );
        }

        private void RefreshSprite()
        {
            image.sprite        = _sfxService.IsSfxEnabled ? activeSprite : inactiveSprite;
            _buttonImage.sprite = _sfxService.IsSfxEnabled ? activeButtonSprite : inactiveButtonSprite;
        }

        private void Reset()
        {
            _button      = GetComponent<Button>();
            _buttonImage = _button.GetComponent<Image>( );
        }
    }
}
