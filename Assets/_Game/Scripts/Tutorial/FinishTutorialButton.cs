using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Tutorial
{
    [RequireComponent(typeof(Button))]
    public class FinishTutorialButton : MonoBehaviour
    {
        private Button _button;

        private ITutorialService _tutorialService;
        
        [Inject]
        private void Construct( ITutorialService tutorialService )
        {
            _button          = GetComponent<Button>( );
            _tutorialService = tutorialService;
            
            _button.onClick.AddListener( FinishTutorial );
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener( FinishTutorial );
        }

        private void FinishTutorial()
        {
            _tutorialService.EndTutorial( );
        }
    }
}
