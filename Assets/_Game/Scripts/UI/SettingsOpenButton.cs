using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class SettingsOpenButton : MonoBehaviour
    {
        public static Action SettingsWindowRequested = delegate{ }; 

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener( OnButtonClicked );
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener( OnButtonClicked );
        }

        private void OnButtonClicked()
        {
            SettingsWindowRequested.Invoke();
        }
    }
}
