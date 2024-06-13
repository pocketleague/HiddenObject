using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Video
{
    public class ItemPanel : MonoBehaviour
    {
        public GameObject pf, content;
        public GameConfig gameConfig;

        void Start()
        {
            SpawnUiItem();
        }

        public void SpawnUiItem()
        {
            foreach (var item in gameConfig.itemConfigs)
            {
                Instantiate(item, content.transform);
            }
        }
    }
}
