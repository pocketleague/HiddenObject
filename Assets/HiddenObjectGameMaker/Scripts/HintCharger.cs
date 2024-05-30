/* HintCharger.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace HOGM
{
	public class HintCharger : MonoBehaviour
	{

		[SerializeField]
		float chargeTimeSec;

		[SerializeField]
		Image imageProgress;
		[SerializeField]
		Button buttonHint;


		bool isCharging;
		float elapsedTime;


		void Start ()
		{
			SetEmpty();
		}
		

		void Update ()
		{
			if (isCharging)
			{
				elapsedTime += Time.deltaTime;
				if (chargeTimeSec <= elapsedTime)
				{
					EndCharging();
				}
				SetProgress();
			}
		}


		void SetProgress ()
		{
			float rate = elapsedTime / (float)chargeTimeSec;
			imageProgress.fillAmount = rate;
		}
			

		public void SetEmpty ()
		{
			elapsedTime = 0f;
			buttonHint.interactable = false;
			SetProgress();
			StartCharging();
		}


		public void StartCharging ()
		{
			isCharging = true;
		}


		public void StopCharging ()
		{
			isCharging = false;
		}


		void EndCharging ()
		{
			isCharging = false;
			buttonHint.interactable = true;
		}

	}
}
