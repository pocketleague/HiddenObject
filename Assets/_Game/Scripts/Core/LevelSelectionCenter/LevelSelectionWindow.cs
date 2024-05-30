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
        [Inject] private LevelSelectionConfig _config;

        private Window _targetWindow;

        [SerializeField] private Button _btnStartGameCenter;
        [SerializeField] private Transform contentParent;
        [SerializeField] private PrefabGridButtonView pf_grid;


        private void Awake()
        {
            _targetWindow = GetComponent<Window>();

            _levelSelectionService.OnLevelSelectionStarted += Open;
            _levelSelectionService.OnLevelSelectionEnded += Close;

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

        void PopulateGrid()
        {
            for (int i = 0; i < _config.levelConfigs.Length; i++)
            {
                PrefabGridButtonView newItem = Instantiate(pf_grid, contentParent);
                newItem.Setup(i, _levelSelectionService);
            }
        }
    }
}