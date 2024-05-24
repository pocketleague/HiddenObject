using Scripts.Stages;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI
{
    [RequireComponent(typeof( Button ) )]
    public class StartGameButton : MonoBehaviour
    {
        [Inject]
        private IStagesService _stagesService;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener( OnButtonClicked );
        }

        private void OnButtonClicked()
        {
            _stagesService.StartCurrentStage();
        }
    }
}