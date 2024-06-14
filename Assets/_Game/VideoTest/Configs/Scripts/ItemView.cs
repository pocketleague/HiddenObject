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

        public GameObject mesh, particlEffect;

        public ItemConfig OnClick(Vector3 hitPos)
        {
            mesh.SetActive(false);
            particlEffect.SetActive(true);
            return itemConfig;
        }

        public void HandleClickable(bool status)
        {
            IsClickable = status;
        }
    }
}