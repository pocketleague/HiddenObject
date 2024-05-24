using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Audio
{
    [RequireComponent(typeof( Button ) )]
    public class MusicEnabledToggle : MonoBehaviour
    {
        private IMusicService _musicService;

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
        private void Construct( IMusicService musicService )
        {
            _musicService = musicService;
            
            _button.onClick.AddListener( OnButtonClicked );

            _musicService.OnMusicEnabledChanged += RefreshSprite;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener( OnButtonClicked );
        }

        private void OnButtonClicked()
        {
            _musicService.ToggleMusicEnabled( );
        }

        private void RefreshSprite()
        {
            image.sprite        = ( _musicService.IsMusicEnabled ? activeSprite : inactiveSprite );
            _buttonImage.sprite = ( _musicService.IsMusicEnabled ? activeButtonSprite : inactiveButtonSprite );
        }
        
        private void Reset()
        {
            _button      = GetComponent<Button>();
            _buttonImage = _button.GetComponent<Image>( );
        }
    }
}
