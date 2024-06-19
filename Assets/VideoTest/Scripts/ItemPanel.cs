using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Video
{
    public class ItemPanel : MonoBehaviour
    {
        public ItemUiView pf_itemUi;
        public Transform content;

        public GameConfig gameConfig;

        public Dictionary<string, ItemUiView> itemList = new Dictionary<string, ItemUiView>();

        void Start()
        {
            GameManager.OnItemClicked += OnItemClicked;
            SpawnUiItem();
        }

        public void SpawnUiItem()
        {
            foreach (var item in gameConfig.itemConfigs)
            {
                ItemUiView itemUiView =  Instantiate(pf_itemUi, content.transform);
                itemUiView.Setup(item);

                itemList.Add(item.itemId , itemUiView);
            }
        }

        void OnItemClicked(ItemConfig itemConfig)
        {
            if (itemList.ContainsKey(itemConfig.itemId))
            {
                Debug.Log("Destroy item "+ itemConfig.itemId);
                //Destroy(itemList[itemConfig.itemId].gameObject);

                itemList[itemConfig.itemId].MarkDone();

                itemList.Remove(itemConfig.itemId);
            }

        }
    }
}
