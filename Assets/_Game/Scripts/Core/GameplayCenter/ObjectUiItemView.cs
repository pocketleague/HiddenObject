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
        [SerializeField] private Image object_Img;

        public int counter;
        public string itemID;

        public void Setup(int count, string _itemId, Sprite objectSprite)
        {
            counter = count;
            txt_count.text = "" + counter;
            object_Img.sprite = objectSprite;
            itemID = _itemId;
        }

        public void ReduceCount()
        {
            counter--;
            txt_count.text = "" + counter;

        }
    }
}