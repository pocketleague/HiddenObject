/* TargetObjectRandomSelector.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using System.Collections;

namespace HOGM
{
	public class TargetObjectRandomSelector : MonoBehaviour
	{
		
		public string keyName;


		TargetObjectsController tgtObjCtrl;


		public void SelectAtRandom (TargetObjectsController ctrl, bool isTarget)
		{
			this.tgtObjCtrl = ctrl;

			if (isTarget)
			{
				if (GameData.Instance.IsCurrentSearchTypeSingle())
				{
					SelectOneAtRandom(true);
				}
				else if (GameData.Instance.IsCurrentSearchTypePair())
				{
					SelectPairAtRandom();
				}
				else if (GameData.Instance.IsCurrentSearchTypeSimilar())
				{
					SelectPluralAtRandom();
				}
			}
			else
			{
				SelectOneAtRandom(false);
			}
		}


		void SelectOneAtRandom (bool isTarget)
		{
			TargetObject[] targetObjects = GetComponentsInChildren<TargetObject>();
			int selectedIndex = Random.Range(0, targetObjects.Length);
			for (int i=0; i<targetObjects.Length; i++)
			{
				if (i == selectedIndex)
				{
					targetObjects[i].gameObject.SetActive(true);
					if (isTarget)
					{
						targetObjects[i].SetKeyNameAndTargetController(keyName, tgtObjCtrl);
					}
				}
				else
				{
					targetObjects[i].gameObject.SetActive(false);
				}
			}
		}


		void SelectPairAtRandom ()
		{
			TargetObject[] targetObjeccts = GetComponentsInChildren<TargetObject>();
			if (targetObjeccts.Length < 2)
			{
				Debug.LogError("2 or more TargetObjectButton required");
				Destroy(this.gameObject);
				return;
			}

			//Shuffle
			GameData.ShuffleArray(targetObjeccts);
			for (int i=0; i<targetObjeccts.Length; i++)
			{
				if (i < 2)
				{
					targetObjeccts[i].gameObject.SetActive(true);
					targetObjeccts[i].SetKeyNameAndTargetController(keyName, tgtObjCtrl);
				}
				else
				{
					targetObjeccts[i].gameObject.SetActive(false);
				}
			}
		}


		void SelectPluralAtRandom ()
		{
			int numOfItems = tgtObjCtrl.answers;

			TargetObject[] targetButtons = GetComponentsInChildren<TargetObject>();
			if (targetButtons.Length < numOfItems)
			{
				Debug.LogError("The number of answers or more TargetObjectButton required");
				Destroy(this.gameObject);
				return;
			}

			//Shuffle
			GameData.ShuffleArray(targetButtons);
			for (int i=0; i<targetButtons.Length; i++)
			{
				if (i < numOfItems)
				{
					targetButtons[i].gameObject.SetActive(true);
					targetButtons[i].SetKeyNameAndTargetController(keyName, tgtObjCtrl);
				}
				else
				{
					targetButtons[i].gameObject.SetActive(false);
				}
			}
		}

	}
}
