/* TargetObject.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using System.Collections;

namespace HOGM
{

	[SelectionBase]
	public class TargetObject : MonoBehaviour
	{

		[SerializeField]
		Animator animator;
		[SerializeField]
		SpriteRenderer spriteRenderer;

		string keyName;
		TargetObjectsController tgtObjCtrl;

		bool isTarget;

		public bool _isColorObject;
		

		readonly int sortingOrderFront = 9000;
		int sortingOrderTmp;


		void Start ()
		{
			
		}



		public int GetSortingOrderInLayer ()
		{
			return spriteRenderer.sortingOrder;
		}


		public void SetKeyNameAndTargetController (string keyName, TargetObjectsController tgtObjCtrl)
		{
			this.keyName = keyName;
			this.tgtObjCtrl = tgtObjCtrl;

			gameObject.name = keyName + "-" + gameObject.name;

			tgtObjCtrl.AddListTargetObject(this);
		}


		public string GetKeyName ()
		{
			return keyName;
		}


		public void SetTargetOn ()
		{
			isTarget = true;
		}


		public bool IsTarget ()
		{
			return isTarget;
		}



		// ------------------------------------------------------------------------------------

		public void OnClick ()
		{
			Debug.Log("OnClick:"+keyName);
			if (tgtObjCtrl == null) return;
			if (!isTarget) return;

			//
			Debug.Log("OnClick:"+keyName);

            if (GameData.Instance.IsCurrentSearchTypeSingle())
            {
                tgtObjCtrl.CheckNameExists(keyName, CheckNameCallbackResult);
            }
            else if (GameData.Instance.IsCurrentSearchTypePair())
            {
                tgtObjCtrl.CheckNameExists(keyName, CheckNameCallbackResultPair);
            }
            else if (GameData.Instance.IsCurrentSearchTypeSimilar())
            {
                tgtObjCtrl.CheckNameExists(keyName, CheckNameCallbackResult);
            }

   //         if (_isColorObject)
   //         {
			//	gameObject.transform.GetChild(1).gameObject.SetActive(true);
			//	gameObject.transform.GetChild(0).gameObject.SetActive(false);
			//}
		}


		void CheckNameCallbackResult (int flagCorrect)
		{
			Debug.Log("flagCorrect:"+flagCorrect.ToString());
			if (flagCorrect == 1)
			{
				isTarget = false;
				StartCorrectAnimation();
			}
		}


		void CheckNameCallbackResultPair (int flagCorrect)
		{
			Debug.Log("flagCorrect:"+flagCorrect.ToString());
			if (flagCorrect == 2)
			{
				isTarget = false;
				StartCorrectAnimation();
				tgtObjCtrl.HideSelectingMark();
			}
			else if (flagCorrect == 1)
			{
				ChangeSelectingState(true);
			}
			else
			{
				ChangeSelectingState(false);
			}
		}


		void ChangeSelectingState (bool isOn)
		{
			if (isOn)
			{
				isTarget = false;
				sortingOrderTmp = spriteRenderer.sortingOrder;
				spriteRenderer.sortingOrder = sortingOrderFront;
				animator.SetBool("Selected", true);

				tgtObjCtrl.ShowSelectingMark(transform.position, spriteRenderer.bounds.size);
			}
			else
			{
				isTarget = true;
				animator.SetBool("Selected", false);
				spriteRenderer.sortingOrder = sortingOrderTmp;
			}
		}


		void StartCorrectAnimation ()
		{
			sortingOrderTmp = spriteRenderer.sortingOrder;
			spriteRenderer.sortingOrder = sortingOrderFront;
			animator.SetTrigger("Play");
		}


		public void EndCorrectAnimation ()
		{
			Destroy(this.gameObject);
		}



		// ------------------------------------------------------------------------------------


	}
}
