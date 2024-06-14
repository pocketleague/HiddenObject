using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Video
{
    public class ItemView : MonoBehaviour
    {
        public GameManager gameManager;
        public ItemConfig itemConfig;

        public bool IsClickable;

        public ItemConfig OnClick(Vector3 hitPos)
        {
            Destroy(gameObject);
            return itemConfig;
        }

        public void HandleClickable(bool status)
        {
            IsClickable = status;
        }
    }
}