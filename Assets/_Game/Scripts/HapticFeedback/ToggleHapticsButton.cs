using System;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.HapticFeedback
{
    [RequireComponent(typeof(Button))]
    public class ToggleHapticsButton : MonoBehaviour
    {
        public static Action<bool> HapticsToggled = delegate{ };

        private Button _button;
        private Image  _buttonImage;
        public  Image  image;

        public Sprite activeSprite;
        public Sprite inactiveSprite;

        public Sprite activeButtonSprite;
        public Sprite inactiveButtonSprite;

        private bool _currentlyActive = true;

        private void Awake()
        {
            _button      = GetComponent<Button>( );
            _buttonImage = _button.GetComponent<Image>( );

            _currentlyActive = PlayerPrefs.GetInt( "HapticsEnabled", MMVibrationManager.HapticsSupported() ? 1 : 0 ) == 1;
            RefreshSprite();

            _button.onClick.AddListener( OnButtonClicked );
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener( OnButtonClicked );
        }

        private void OnButtonClicked()
        {
            _currentlyActive = !_currentlyActive;
            RefreshSprite();

            PlayerPrefs.SetInt( "HapticsEnabled", _currentlyActive ? 1 : 0 );
            HapticsToggled.Invoke( _currentlyActive );
        }

        private void RefreshSprite()
        {
            image.sprite        = ( _currentlyActive ? activeSprite : inactiveSprite );
            _buttonImage.sprite = ( _currentlyActive ? activeButtonSprite : inactiveButtonSprite );
        }
    }
}
