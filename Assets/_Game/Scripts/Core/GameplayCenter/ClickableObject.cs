using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
	TargetObjectPrefabView _targetObjectPrefabView = null;

	public void Setup(TargetObjectPrefabView targetObjectPrefabView)
    {
		_targetObjectPrefabView = targetObjectPrefabView;
		gameObject.layer = 6;
	}

	public void OnClick()
	{
		_targetObjectPrefabView.FoundObject();
	}


}
