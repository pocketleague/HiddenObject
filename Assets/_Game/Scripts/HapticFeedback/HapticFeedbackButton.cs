using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.HapticFeedback
{
    [RequireComponent(typeof(Button))]
    public class HapticFeedbackButton : MonoBehaviour
    {
        public static Action RequestHapticFeedback = delegate{ };

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
            RequestHapticFeedback.Invoke();
        }
    }
}
