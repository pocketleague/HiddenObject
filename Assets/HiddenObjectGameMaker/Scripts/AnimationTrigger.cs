/* AnimationTrigger.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace HOGM
{	
	public class AnimationTrigger : MonoBehaviour
	{
		[SerializeField]
		UnityEvent OnStartAnimation;
		[SerializeField]
		UnityEvent OnEndAnimation;
		[SerializeField]
		UnityEvent OnEndShowAnimation;
		[SerializeField]
		UnityEvent OnEndHideAnimation;


		public void StartAnimation ()
		{
			OnStartAnimation.Invoke();
		}


		public void EndAnimation ()
		{
			OnEndAnimation.Invoke();
		}


		public void EndShowAnimation ()
		{
			OnEndShowAnimation.Invoke();
		}


		public void EndHideAnimation ()
		{
			OnEndHideAnimation.Invoke();
		}



		// ------------------------------------------------------------------------------------

		public void PlaySEStarShown ()
		{
			SoundManager.Instance.PlaySEStar();
		}

		public void PlaySEBestShown ()
		{
			SoundManager.Instance.PlaySEBest();
		}

	}
}
