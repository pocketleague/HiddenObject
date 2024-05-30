/* ListItem.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace HOGM
{
	public class ListItem : MonoBehaviour
	{
		
		[SerializeField]
		Text textItemName;
		[SerializeField]
		Image imageSilhouette;
		[SerializeField]
		Image pairMark;


		string keyName;
		Sprite siluet;
		bool isDisplayAnimationOnStart;
		CanvasGroup canvGp;
		Animator animator;
		TargetObjectsController tgtObjCtrl;


		void Start ()
		{
			textItemName.text = keyName;
			imageSilhouette.sprite = siluet;

			//
			if (GameData.Instance.IsCurrentListItemTypeWords())
			{
				textItemName.gameObject.SetActive(true);
				imageSilhouette.gameObject.SetActive(false);
			}
			else if (GameData.Instance.IsCurrentListItemTypeSilhouettes())
			{
				textItemName.gameObject.SetActive(false);
				imageSilhouette.gameObject.SetActive(true);
			}

			//
			pairMark.gameObject.SetActive(GameData.Instance.IsCurrentSearchTypePair());

			canvGp = GetComponent<CanvasGroup>();
			animator = GetComponent<Animator>();

			canvGp.alpha = 1.0f;
			if (isDisplayAnimationOnStart)
			{
				canvGp.alpha = 0.0f;
				animator.SetTrigger("Show");
			}
		}


		public void SetItemName (string name, TargetObjectsController toc, bool isDisplayAnimation, Sprite siluet = null)
		{
			this.keyName = name;
			this.isDisplayAnimationOnStart = isDisplayAnimation;
			this.tgtObjCtrl = toc;

			this.siluet = siluet;
		}


		public void OnCorrect ()
		{
			animator.SetTrigger("Play");
		}


		public void EndCorrectAnimation ()
		{
			tgtObjCtrl.DeleteListItemAndAddNext(keyName, transform.GetSiblingIndex());
		}


		public void EndCorrectAnimationForced (float waitTime)
		{
			StartCoroutine(EndCorrectAnimationForcedWithDelay(waitTime));
		}


		IEnumerator EndCorrectAnimationForcedWithDelay (float waitTime)
		{
			yield return new WaitForSeconds(waitTime);

			EndCorrectAnimation();
		}
	}
}
