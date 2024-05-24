using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class SettingsWindow : MonoBehaviour
    {
        public static Action<bool> RequestInputBlock = delegate{ };

        private Window _targetWindow;

        public Button privacyPolicyButton;
        public Button termsOfUseButton;

        private const string _privacyPolicyUrl = "https://www.crazylabs.com/website-privacy-policy/";
        private const string _termsOfUseUrl    = "https://www.crazylabs.com/crazylabs-site-terms-of-use/";

        private void Awake()
        {
            _targetWindow = GetComponent<Window>();

            _targetWindow.Opened += OnWindowOpened;
            _targetWindow.Closed += OnWindowClosed;

            privacyPolicyButton.onClick.AddListener( OnPrivacyButtonPressed );
            termsOfUseButton.onClick.AddListener( OnTermsButtonPressed );

            SettingsOpenButton.SettingsWindowRequested += OnSettingsWindowRequested;
        }

        private void OnDestroy()
        {
            _targetWindow.Opened -= OnWindowOpened;
            _targetWindow.Closed -= OnWindowClosed;

            SettingsOpenButton.SettingsWindowRequested -= OnSettingsWindowRequested;
        }

        private void OnSettingsWindowRequested()
        {
            _targetWindow.Open();
        }

        private void OnWindowOpened()
        {
            RequestInputBlock.Invoke( true );
        }

        private void OnWindowClosed()
        {
            RequestInputBlock.Invoke( false );
        }

        private void OnPrivacyButtonPressed()
        {
            Application.OpenURL( _privacyPolicyUrl );
            return;
        }

        private void OnTermsButtonPressed()
        {
            Application.OpenURL( _termsOfUseUrl );
        }
    }
}
