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
	[SerializeField] private GameObject			itemHolder;
	[SerializeField] private ClickableObject	clickableObject;
	[SerializeField] private GameObject			clickParticle;
	GameObject particleEffect;

	[Inject] private IGameplayCenterService _gameplayCenterService;

	public void SetUp(ItemStateData itemStateData, LevelPrefabView levelPrefabView)
	{
		_itemStateData = itemStateData;
		_levelPrefabView = levelPrefabView;
		gameObject.name = itemStateData.itemID + "-" + gameObject.name;

		clickableObject.Setup(this);
	}

	public void FoundObject(Vector3 hitPoint)
	{
		 particleEffect =  Instantiate(clickParticle, hitPoint, Quaternion.identity);
		//Vector3 dir = Camera.main.transform.position - particleEffect.transform.position;
		particleEffect.transform.LookAt(Camera.main.transform,Vector3.up);

		_gameplayCenterService.FoundObject(_itemStateData);

		LeanTween.scale(gameObject, Vector3.zero, 0.5f).setEaseInCirc().setOnComplete(()=>
		{
			DestroyObject();
		});
		//_levelPrefabView.FoundObject(_itemStateData);
		//Destroy(gameObject);
	}

	void DestroyObject()
    {
		Destroy(particleEffect);
		Destroy(gameObject);

	}
}
