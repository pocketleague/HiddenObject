using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;
using Scripts.UI;

namespace Scripts.Core.LevelSelection
{
    public class LevelSelectionWindow : MonoBehaviour
    {
        [Inject] private ILevelSelectionService _levelSelectionService;
        [Inject] private IStateManagerService _stateManagerService;
        private Window _targetWindow;

        [SerializeField]
        private Button _btnStartGameCenter;

        private void Awake()
        {
            _targetWindow = GetComponent<Window>();

            _levelSelectionService.OnLevelSelectionStarted += Open;
            _levelSelectionService.OnLevelSelectionEnded += Close;

            _btnStartGameCenter.onClick.AddListener(StartGameCenterClicked);
        }

        private void OnDestroy()
        {
            _levelSelectionService.OnLevelSelectionStarted -= Open;
            _levelSelectionService.OnLevelSelectionEnded -= Close;
        }

        void Open()
        {
            _targetWindow.Open();
        }

        void Close()
        {
            _targetWindow.Close();
        }

        void StartGameCenterClicked()
        {
            _stateManagerService.ChangeState(EState.GameplayCenter);
        }
    }
}