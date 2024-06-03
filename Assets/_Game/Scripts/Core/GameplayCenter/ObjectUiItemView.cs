using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scripts.Core.LevelSelection;

namespace GameplayCenter
{
    public class ObjectUiItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txt_count;

        public int counter;
        public string itemID;

        public void Setup(int count, string _itemId)
        {
            counter = count;
            txt_count.text = "" + counter;

            itemID = _itemId;
        }

        public void ReduceCount()
        {
            counter--;
            txt_count.text = "" + counter;

        }
    }
}