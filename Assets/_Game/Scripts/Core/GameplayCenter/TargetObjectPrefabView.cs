using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Core.LevelSelection;
using Zenject;
using GameplayCenter;

public class TargetObjectPrefabView : MonoBehaviour
{
	ItemStateData _itemStateData;
	LevelPrefabView _levelPrefabView;
	[SerializeField] private GameObject itemHolder;
	[SerializeField] private ClickableObject clickableObject;

	[Inject] private IGameplayCenterService _gameplayCenterService;

	public void SetUp(ItemStateData itemStateData, LevelPrefabView levelPrefabView)
	{
		_itemStateData = itemStateData;
		_levelPrefabView = levelPrefabView;
		gameObject.name = itemStateData.itemID + "-" + gameObject.name;

		clickableObject.Setup(this);
	}

	public void FoundObject()
	{
		_gameplayCenterService.FoundObject(_itemStateData);
		//_levelPrefabView.FoundObject(_itemStateData);
		Destroy(gameObject);
	}
}
