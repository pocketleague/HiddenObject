using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Video
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/Video/ItemConfig")]
    public class ItemConfig : ScriptableObject
    {
        public string itemId;
        public Sprite itemSprite;
    }
}
