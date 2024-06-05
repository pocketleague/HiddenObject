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
        public int childPosIndex;
        public void Setup(int count, string _itemId, Sprite objectSprite)
        {
           
            counter = count;
            txt_count.text = "" + counter;
            object_Img.sprite = objectSprite;
            itemID = _itemId;
        }

        public void SetChildIndex(int childPos)
        {
            childPosIndex = childPos;
        }

        public void ReduceCount()
        {
            object_Img.gameObject.transform.localScale = Vector3.one;
            LeanTween.scale(object_Img.gameObject,new Vector3(0.2f,0.2f,0.2f),0.5f).setEasePunch();

            counter--;
            txt_count.text = "" + counter;

        }
    }
}