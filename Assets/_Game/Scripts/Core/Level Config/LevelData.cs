using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Scripts.Core.GameplayCenter;

namespace Scripts.Core.LevelSelection
{
	public enum ListItemType
	{
		Words,
		Silhouettes
	}

	public enum SearchType
	{
		Single,
		Pair,
		Similar
	}

	[Serializable]
	public struct LevelData
    {
		public ListItemType listItemType;
		public SearchType searchType;
		public float totalTime;
		public int totalObjective;
		public LevelPrefabView levelPrefabView;

	}

	[Serializable]
	public struct LevelItemData
    {
		public string id;
		public List<TargetObjectPrefabView> item;
		public Sprite itemSprite;
	}

	[Serializable]
	public struct ItemStateData
    {
		public GameObject item;
		public String itemID;
		public bool found;
    }
		
}