using System;
using UnityEngine;

namespace Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Window : MonoBehaviour
    {
        public Action Opened = delegate { };
        public Action Closed = delegate { };

        private CanvasGroup _canvasGroup;

        private const float _animationSpeed = 3f;
        private float _animationTimer = 0f;

        public string id;
        public bool openOnStart = false;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            if (openOnStart)
                OpenInstantly();
        }

        public bool IsOpened { get; private set; }

        public void Open()
        {
            if (IsOpened)
                return;

            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            IsOpened = true;
            _animationTimer = 0f;

            Opened.Invoke();
        }

        public void Open(float delay)
        {
            if (IsOpened)
                return;

            IsOpened = true;
            _animationTimer = delay * -_animationSpeed;
        }

        public void Close()
        {
            if (!IsOpened)
                return;

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            IsOpened = false;
            _animationTimer = 1f;

            Closed.Invoke();
        }

        public void OpenInstantly()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            IsOpened = true;
            _animationTimer = 1f;
            _canvasGroup.alpha = 1f;

            Opened.Invoke();
        }

        public void Close(float delay)
        {
            if (!IsOpened)
                return;

            Close();
            _animationTimer = 1 + delay * _animationSpeed;
        }

        private void Update()
        {
            _animationTimer += Time.unscaledDeltaTime * (IsOpened ? _animationSpeed : -_animationSpeed);
            _canvasGroup.alpha = Mathf.Lerp(0f, 1f, _animationTimer);

            if (IsOpened && !_canvasGroup.interactable && _canvasGroup.alpha == 1f)
            {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            }

            if (PlayerPrefs.GetInt("HUD_Hidden", 0) == 1)
                _canvasGroup.alpha = 0;
        }
    }
}
