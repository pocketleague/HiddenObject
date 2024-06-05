using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Core.LevelSelection;
using System;

public class LevelPrefabView : MonoBehaviour
{
    public List<LevelItemData> levelItemDatas;
    [SerializeField] private List<ItemStateData> itemStateDatas = new List<ItemStateData>();
    public int activeObjectIndex = -1;
    public void OnSetUp(Action callBack)
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

        for(int i =0;i < 3; i++)
        {
            ActiveNewObject();
        }

        callBack.Invoke();
    }

    public void FoundObject(ItemStateData itemStateData)
    {
        if(itemStateDatas.Contains(itemStateData))
        {
            itemStateDatas.Remove(itemStateData);
            ActiveNewObject();
        }
    }

    public void ActiveNewObject()
    {
        if(activeObjectIndex < itemStateDatas.Count)
        {
            itemStateDatas[activeObjectIndex].item.GetComponent<TargetObjectPrefabView>().EnableClickable();
            activeObjectIndex++;
        }
       
    }

    private void OnDisable()
    {
        
    }


}



