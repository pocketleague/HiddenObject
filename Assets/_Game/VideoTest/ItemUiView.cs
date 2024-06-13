using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Video
{
    public class ItemUiView : MonoBehaviour
    {
        public Image img;

        public void Setup(ItemConfig itemConfig)
        {
            img.sprite = itemConfig.itemSprite;
        }
    }
}