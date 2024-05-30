/* TargetObjectsController.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HOGM
{
	
	public class TargetObjectsController : MonoBehaviour
	{
		
		[System.Serializable]
		public class TargetData
		{
			public string keyName;
			public Sprite spriteSilhouette;
		}

		[SerializeField]
		TargetData[] targetDataList;

		[SerializeField, Space(15)]
		Transform targetListContent;
		[SerializeField]
		Transform targetListContentForPlural;
		[SerializeField]
		Text textFoundItems;
		[SerializeField]
		Image imageFoundItemsProgress;

		[SerializeField, Space(15)]
		GameObject prefabListItem;
		[SerializeField]
		GameObject prefabSelectingMark;
		[SerializeField]
		GameObject prefabHintAreaMark;

		[SerializeField, Space(15)]
		int maxDisplayItems = 6;

		public int answers {get; private set;}

		[SerializeField, Space(10)]
		UnityEvent OnGameClearedJust;
		[SerializeField]
		UnityEvent OnEndLastItemCorrectAnimation;


		//
		GameData gd;

		Dictionary<string, TargetData> dicTargetData = new Dictionary<string, TargetData>();

		List<string> listRemainingTarget = new List<string>();
		HashSet<string> hashInitialTargets = new HashSet<string>();
		Dictionary<string, ListItem> dicListItem = new Dictionary<string, ListItem>();
		List<TargetObject> listTargetObject = new List<TargetObject>();


		Transform tfListContent;

		int counterHits = 0;


		// Used for Pair Mode
		string selectedName = ""; 
		Action<int> selectedCallbackResult; 
		GameObject objSelectingMark;


		void Start ()
		{
			gd = GameData.Instance;

			answers = gd.numberOfAnswers;
			textFoundItems.gameObject.SetActive(false);
			imageFoundItemsProgress.gameObject.SetActive(false);
			StartCoroutine(SetFoundItemsProgress(0, 0.0f));

			// Change List Content by Search Type
			if (gd.IsCurrentSearchTypeSimilar())
			{
				tfListContent = targetListContentForPlural;
				targetListContent.gameObject.SetActive(false);
			}
			else
			{
				tfListContent = targetListContent;
				targetListContentForPlural.gameObject.SetActive(false);
			}

			//
			SetTargetDataDictionary();

			//
			ShuffleAndSetTargetList();

			//
			DecideTargetObjectButtons();

			//
			if (gd.IsCurrentSearchTypeSingleOrPair())
			{
				SetListItem(true, "", -1);
			}
			else if (gd.IsCurrentSearchTypeSimilar())
			{
				SetListItem(true, "", 0);
			}
		}


		void SetTargetDataDictionary ()
		{
			foreach (TargetData tdata in targetDataList)
			{
				if (!dicTargetData.ContainsKey(tdata.keyName))
				{
					dicTargetData.Add(tdata.keyName, tdata);
				}
			}
		}


		void ShuffleAndSetTargetList ()
		{
			if (gd.IsCurrentSearchTypeSimilar())
			{
				// **** Use first target ****
				listRemainingTarget.Add(targetDataList[0].keyName);
				SetHashInitialTargets();
				return;
			}

			//
			List<string> keyList = new List<string>(dicTargetData.Keys);
			string[] keys = keyList.ToArray();
			GameData.ShuffleArray(keys);

			answers =  Mathf.Min(answers, keys.Length);
			for (int i=0; i< answers; i++)
			{
				listRemainingTarget.Add(keys[i]);
			}
			SetHashInitialTargets();
		}


		void SetHashInitialTargets ()
		{
			foreach (string name in listRemainingTarget)
			{
				hashInitialTargets.Add(name);
			}
		}


		void DecideTargetObjectButtons ()
		{
			TargetObjectRandomSelector[] randomSelectors = GetComponentsInChildren<TargetObjectRandomSelector>();
			foreach (TargetObjectRandomSelector selector in randomSelectors)
			{
				selector.SelectAtRandom(this, hashInitialTargets.Contains(selector.keyName));
			}
		}


		public void AddListTargetObject (TargetObject targetObj)
		{
			listTargetObject.Add(targetObj);
		}


		// ------------------------------------------------------------------------------------

		void SetListItem (bool isInit, string deleteItemName, int sibilingIndex)
		{
			if (deleteItemName != "")
			{
				if (dicListItem.ContainsKey(deleteItemName))
				{
					DestroyListItemObject(dicListItem[deleteItemName].gameObject);
					dicListItem.Remove(deleteItemName);
				}
			}

			foreach (string name in listRemainingTarget)
			{
				if (maxDisplayItems <= dicListItem.Count) break;

				InstantiateListItem(name, sibilingIndex);

				if (!isInit) break;
			}

			foreach (string name in dicListItem.Keys)
			{
				listRemainingTarget.Remove(name);
			}

			//
			if (!isInit && dicListItem.Count < maxDisplayItems)
			{
				InstantinateDummyItem(sibilingIndex);
			}
		}


		void DestroyListItemObject (GameObject obj)
		{
			if (gd.IsCurrentSearchTypeSingleOrPair())
			{
				Destroy(obj);
			}
		}


		void InstantiateListItem (string name, int sibilingIndex)
		{
			GameObject objItem = Instantiate(prefabListItem);
			objItem.name = name;
			ListItem item = objItem.GetComponent<ListItem>();
			objItem.transform.SetParent(tfListContent, false);
			bool isReplaceMode = (0 <= sibilingIndex); // true : Replace GameObject(ListItem) at same position
			if (isReplaceMode)
			{
				objItem.transform.SetSiblingIndex(sibilingIndex);
			}
			TargetData tdata = dicTargetData[name];
			item.SetItemName(name, this, isReplaceMode, tdata.spriteSilhouette);
			dicListItem.Add(name, item);
			SetTargetOn(name);
		}


		void InstantinateDummyItem (int sibilingIndex)
		{
			GameObject objDummyItem = new GameObject("dummy");
			objDummyItem.AddComponent<RectTransform>();
			objDummyItem.transform.SetParent(tfListContent);
			objDummyItem.transform.SetSiblingIndex(sibilingIndex);
		}


		void SetTargetOn (string name)
		{
			foreach (TargetObject tobj in listTargetObject)
			{
				if (name == tobj.GetKeyName())
				{
					tobj.SetTargetOn();
				}
			}
		}



		// ------------------------------------------------------------------------------------

		public void CheckNameExists (string name, Action<int> CallbackResult)
		{
			if (gd.IsCurrentSearchTypeSingle())
			{
				if (dicListItem.ContainsKey(name))
				{
					CallbackResult(1);
					OnCorrect(name);
				}
				else
				{
					CallbackResult(0);
				}

			}
			else if (gd.IsCurrentSearchTypePair())
			{
				if (selectedName == name)
				{
					CallbackResult(2);
					selectedCallbackResult(2);
					selectedName = "";
					selectedCallbackResult = null;
					OnCorrect(name);

				}
				else
				{
					if (dicListItem.ContainsKey(name))
					{
						CallbackResult(1);
						if (selectedName == "")
						{
							selectedName = name;
							selectedCallbackResult = CallbackResult;
							SoundManager.Instance.PlaySESelect();
						}
						else
						{
							selectedCallbackResult(0); //cancel

							selectedName = name;
							selectedCallbackResult = CallbackResult;
							SoundManager.Instance.PlaySESelect();
						}
					}
					else
					{
						CallbackResult(0);
					}
				}
			}
			else if (gd.IsCurrentSearchTypeSimilar())
			{
				if (dicListItem.ContainsKey(name))
				{
					CallbackResult(1);
					OnCorrectForSearchTypePlural(name);
				}
				else
				{
					CallbackResult(0);
				}
			}
		}


		void OnCorrect (string name) // SearchType : One , Pair
		{
			SoundManager.Instance.PlaySECorrect();
			counterHits++;
			SetFoundItemsProgressWithDelay(counterHits);

			if (counterHits == answers)
			{
				OnGameClearedJust.Invoke();
			}

			dicListItem[name].OnCorrect();
		}


		void OnCorrectForSearchTypePlural (string name) // SearchType : Plural
		{
			SoundManager.Instance.PlaySECorrect();
			counterHits++;
			SetFoundItemsProgressWithDelay(counterHits);

			if (counterHits == answers)
			{
				OnGameClearedJust.Invoke();
				dicListItem[name].EndCorrectAnimationForced(2.0f);
			}
		}


		void SetFoundItemsProgressWithDelay (int hits)
		{
			StartCoroutine(SetFoundItemsProgress(hits, 1.6f));
		}


		IEnumerator SetFoundItemsProgress (int hits, float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			textFoundItems.text = hits.ToString() + " / " + answers.ToString();
			imageFoundItemsProgress.fillAmount = (float)hits / (float)answers;

			textFoundItems.gameObject.SetActive(true);
			imageFoundItemsProgress.gameObject.SetActive(true);
		}


		// ------------------------------------------------------------------------------------

		public void DeleteListItemAndAddNext (string deleteItemName, int sibilingIndex)
		{
			SetListItem(false, deleteItemName, sibilingIndex);
			if (dicListItem.Count == 0)
			{
				OnEndLastItemCorrectAnimation.Invoke();
			}
		}



		// ------------------------------------------------------------------------------------

		public void ShowSelectingMark (Vector3 position, Vector3 size)
		{
			if (objSelectingMark != null) Destroy(objSelectingMark);

			objSelectingMark = Instantiate(prefabSelectingMark);
			objSelectingMark.transform.SetParent(this.transform, false);
			objSelectingMark.transform.localScale = Vector3.one;
			float selectingMarkSize = objSelectingMark.GetComponent<SpriteRenderer>().bounds.size.x;

			objSelectingMark.transform.position = position;
			float markSize = 1.6f * size.x / selectingMarkSize; // adjusting size
			objSelectingMark.transform.localScale = new Vector3 (markSize, markSize, 1f);
		}


		public void HideSelectingMark ()
		{
			objSelectingMark.SetActive(false);
		}




		// ------------------------------------------------------------------------------------

		public void OnClickHint ()
		{
			TargetObject target = null;

			if (gd.IsCurrentSearchTypePair() && selectedName != "")
			{
				foreach (TargetObject tobj in listTargetObject)
				{
					if (tobj.GetKeyName() == selectedName && tobj.IsTarget())
					{
						target = tobj;
						break;
					}
				}
			}
			else
			{
				TargetObject[] targetObjects = listTargetObject.ToArray();
				GameData.ShuffleArray(targetObjects);
				for (int i=0; i<targetObjects.Length; i++)
				{
					if (targetObjects[i].IsTarget())
					{
						target = targetObjects[i];
						break;
					}
				}
			}
				
			if (target == null) return;	// No target

			ShowHintAreaMark(target.transform.position);

			SoundManager.Instance.PlaySEHint();
		}


		void ShowHintAreaMark (Vector3 position)
		{
			GameObject obj = Instantiate(prefabHintAreaMark);
			obj.transform.SetParent(this.transform, false);
			obj.transform.position = position;
			obj.SetActive(true);
		}

	}

}
