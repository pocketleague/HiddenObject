using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;
using Scripts.UI;
using TMPro;

namespace Scripts.Core.LevelSelection
{
    public class LevelSelectionWindow : MonoBehaviour
    {
        [Inject] private ILevelSelectionService _levelSelectionService;
        [Inject] private IStateManagerService _stateManagerService;
        private Window _targetWindow;


        [SerializeField] private Button _btnStartGameCenter;
        [SerializeField] private Transform contentParent;
        [SerializeField] private GameObject pf_grid;


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

            PopulateGrid();
        }

        void Close()
        {
            _targetWindow.Close();
        }

        void StartGameCenterClicked()
        {
            _stateManagerService.ChangeState(EState.GameplayCenter);
        }

        void PopulateGrid()
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject newItem = Instantiate(pf_grid, contentParent);
                // Optionally, set data on the new item (e.g., update text or images)
                newItem.GetComponentInChildren<TextMeshProUGUI>().text = "Item " + (i + 1);
            }
        }
    }
}