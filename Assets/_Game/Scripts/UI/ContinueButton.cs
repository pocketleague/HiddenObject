using Scripts.Stages;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class ContinueButton : MonoBehaviour
    {
        [Inject]
        private IStagesService _stagesService;

        private Button _button;
        private Window _parentWindow;

        private float _interactiveTimer = 1f;

        private void Awake()
        {
            _button       = GetComponent<Button>();
            _parentWindow = GetComponentInParent<Window>();
            _parentWindow.Opened += OnWindowOpened;

            _button.onClick.AddListener( OnButtonClicked );
        }

        private void OnDestroy()
        {
            _parentWindow.Opened -= OnWindowOpened;
        }

        private void Update()
        {
            _interactiveTimer -= Time.deltaTime;
        }

        private void OnWindowOpened()
        {
            _interactiveTimer = 1f;
        }

        private void OnButtonClicked()
        {
            if ( _interactiveTimer > 0 )
                return;
            
            _stagesService.SpawnCurrentStage();
        }
    }
}