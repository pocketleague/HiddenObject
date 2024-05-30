using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HiddenObject.LevelData;


public class LevelPrefabView : MonoBehaviour
{
    public List<LevelItemData> levelItemDatas;
    [SerializeField] private List<ItemStateData> itemStateDatas = new List<ItemStateData>();
    

    public void OnEnable()
    {
        OnSetUp();
    }

    void OnSetUp()
    {
        for(int i =0; i< levelItemDatas.Count; i++ )
        {
            for (int j = 0; j < levelItemDatas[i].item.Count; j++)
            {
                ItemStateData itemStateData = new ItemStateData();
                itemStateData.item = levelItemDatas[i].item[j].gameObject;
                itemStateData.itemID = levelItemDatas[i].id;
                itemStateData.found = false;

                levelItemDatas[i].item[j].SetUp(itemStateData, this);

                itemStateDatas.Add(itemStateData);
            }
        }
    }

    public void FoundObject(ItemStateData itemStateData)
    {
        if(itemStateDatas.Contains(itemStateData))
        {
            itemStateDatas.Remove(itemStateData);
        }
    }


    private void OnDisable()
    {
        
    }


}



