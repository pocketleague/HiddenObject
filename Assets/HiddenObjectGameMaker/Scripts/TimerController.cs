/* TimerController.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

namespace HOGM
{
		
	public class TimerController : MonoBehaviour
	{

		[SerializeField, Space(10)]
		int limitedTimeSec = 300;

		[SerializeField, Space(15)]
		Text textTime;
		[SerializeField]
		Image imageProgress;

		[SerializeField, Space(10)]
		RectTransform[] rectStars;

		[SerializeField, Space(10)]
		float[] positionStars; // % (0.0 - 1.0) left = 0

		[SerializeField, Space(20)]
		UnityEvent Ended;
		[SerializeField]
		UnityEvent[] PassedStarPoints;

		public bool isOn {get; private set;}

		float elapsedTime;
		bool[] hasStar;


		void Awake ()
		{
			isOn = false;
		}


		void Start ()
		{
			ResetTimer();
		}


		void SetStarPosition ()
		{
			if (rectStars.Length != positionStars.Length)
			{
				Debug.LogError("Set the same number of RectTransform and Position.");
				return;
			}

			hasStar = new bool[rectStars.Length];

			RectTransform rectBase = imageProgress.GetComponent<RectTransform>();
			for (int i=0; i<rectStars.Length; i++)
			{
				Vector2 newPos = rectStars[i].anchoredPosition;
				newPos.x = rectBase.rect.width * positionStars[i] - rectBase.rect.width*0.5f;
				rectStars[i].anchoredPosition = newPos;

				hasStar[i] = true;
			}
		}
			

		void Update ()
		{
			if (isOn)
			{
				elapsedTime += Time.deltaTime;
				if (limitedTimeSec <= elapsedTime)
				{
					Debug.Log("End --- elapsed time:"+elapsedTime.ToString("F3"));
//					TripleTapGames.Instance.SetProgression(GameAnalyticsSDK.GAProgressionStatus.Fail, GameData.Instance.currentLevel);
					Ended.Invoke();
					isOn = false;
				}
				textTime.text = FormatTimeRemaining();
				SetProgress();
				CheckStarPointAndInvokeEvent();
			}
		}


		string FormatTimeRemaining ()
		{
			float fSecRemaining = (float)limitedTimeSec - elapsedTime;
			int secRemaining = (int)fSecRemaining;
			if (secRemaining < 0) secRemaining = 0;
			int minutes = secRemaining / 60;
			int seconds = secRemaining - minutes*60;

			return minutes.ToString("D2") + ":" + seconds.ToString("D2");
		}


		public string FormatTimeRemainingForGameClear ()
		{
			float fSecRemaining = (float)limitedTimeSec - elapsedTime;
			int secRemaining = (int)fSecRemaining;
			if (secRemaining < 0) secRemaining = 0;
			int minutes = secRemaining / 60;
			int seconds = secRemaining - minutes*60;
			int sec100 = (int)((fSecRemaining - (float)secRemaining)*100);

			//<b>00:00</b><size=50>.00</size>
			return string.Format("<b>{0:D2}:{1:D2}</b><size=50>.{2:D2}</size>", minutes, seconds, sec100);
		}

		public int GetRemainingTimeMillsecond ()
		{
			return (int)(((float)limitedTimeSec - elapsedTime) * 1000);
		}


		void SetProgress ()
		{
			float rate = 1f - (elapsedTime / (float)limitedTimeSec);
			imageProgress.fillAmount = rate;
		}


		void CheckStarPointAndInvokeEvent ()
		{
			for (int i=0; i<positionStars.Length; i++)
			{
				if (hasStar[i] && imageProgress.fillAmount < positionStars[i])
				{
					hasStar[i] = false;
					PassedStarPoints[i].Invoke();
				}
			}
		}



		// ------------------------------------------------------------------------------------

		public int GetNumberOfStars ()
		{
			int counter = 0;
			for (int i=0; i<hasStar.Length; i++)
			{
				if (hasStar[i]) counter++;
			}

			return counter;
		}


		// ------------------------------------------------------------------------------------

		public void ResetTimer ()
		{
			elapsedTime = 0f;
			SetStarPosition();
			textTime.text = FormatTimeRemaining();
			SetProgress();
		}


		public void StartTimer ()
		{
			isOn = true;
		}


		public void StopTimer ()
		{
			isOn = false;
		}


		public void MinusSec (float sec)
		{
			elapsedTime += sec;
		}
	
	}

}
