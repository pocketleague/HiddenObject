using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace Scripts.Core.LevelSelection
{
    public class PrefabGridButtonView : MonoBehaviour
    {
        private ILevelSelectionService _levelSelectionService;

        [SerializeField] TextMeshProUGUI txt_itemNo;

        [SerializeField] Button _button;

        private int _index;

        public void Setup(int index, ILevelSelectionService levelSelectionService)
        {
            _levelSelectionService = levelSelectionService;

            _index = index;

            txt_itemNo.text = "Item " + (index + 1);
            _button.onClick.AddListener(SelectLevel);
        }

        void SelectLevel()
        {
            _levelSelectionService.SelectLevel(_index);
        }
    }
}
