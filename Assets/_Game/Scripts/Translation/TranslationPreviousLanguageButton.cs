using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Translation
{
    [RequireComponent(typeof( Button ) )]
    public class TranslationPreviousLanguageButton : MonoBehaviour
    {
        [Inject]
        private ITranslationService _translationService;
        
        private Button _buttonCached;

        private void Awake()
        {
            _buttonCached = GetComponent<Button>();

            _buttonCached.onClick.AddListener( OnButtonClicked );
        }

        private void OnDestroy()
        {
            _buttonCached.onClick.RemoveListener( OnButtonClicked );
        }

        private void OnButtonClicked()
        {
            _translationService.ChangeLanguageToPrevious();
        }
    }
}

