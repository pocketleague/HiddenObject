/* GameController.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace HOGM
{
	public class GameController : MonoBehaviour
	{
		// GUI
		[SerializeField, Space(10), HeaderAttribute ("GUI")]
		Text textLevelNumber;
		[SerializeField]
		GameObject panelReady;
		[SerializeField]
		Text textStartMessage;
		[SerializeField]
		GameObject panelPauseOverClear;
		[SerializeField]
		GameObject objClearTimeRemaining;
		[SerializeField]
		GameObject objClearStars;
		[SerializeField]
		GameObject objClearButtons;
		[SerializeField]
		Text textClearTime;

		[SerializeField, Space(10)]
		TimerController timerCtrl;

		[SerializeField, Space(10)]
		float penaltySeconds;
		[SerializeField]
		int penaltyClicks;
		[SerializeField]
		float penaltyResetTime;
		[SerializeField]
		GameObject prefabMinusTimeMark;


		//
		GameData gd;

		int counterMisclick;
		float penaltyJudgeTime;

		bool isClearTimeBest;



		void Start ()
		{
			gd = GameData.Instance;

			textLevelNumber.text = "Lv " + gd.currentLevel;
			panelPauseOverClear.SetActive(false);
			ShowReadyPanel();

		}


		void ShowReadyPanel ()
		{
			SetStartText();

			StartAnimationPanel(panelReady, "Show", 0.0f);

			StartCoroutine(PlayStartSEWithDelay(0.4f));
		}


		IEnumerator PlayStartSEWithDelay (float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			SoundManager.Instance.PlaySEStart();
		}


		void SetStartText ()
		{
			string mode = "";
			if (gd.IsCurrentListItemTypeWords())
			{
				mode = "[ Word Mode ]";
			}
			else if (gd.IsCurrentListItemTypeSilhouettes())
			{
				mode = "[ Silhouette Mode ]";
			}

			string message = "";
			string numAnswer = "";
			string displayed = (Screen.width < Screen.height) ? "displayed below!" : "displayed left side!";
			if (gd.IsCurrentSearchTypeSingle())
			{
				message = "Find all the objects \n " + displayed;
				numAnswer = "(" + gd.numberOfAnswers.ToString() + " items)";
			}
			else if (gd.IsCurrentSearchTypePair())
			{
				message = "Find all the pairs of objects \n " + displayed;
				numAnswer = "(" + gd.numberOfAnswers.ToString() + " pairs)";
			}
			else if (gd.IsCurrentSearchTypeSimilar())
			{
				message = "Find all the similar objects \n " + displayed;
				numAnswer = "(x " + gd.numberOfAnswers.ToString() + ")";
			}

			textStartMessage.text = mode + "\n\n" + message + "\n\n" + numAnswer;
		}


		void HideReadyPanel ()
		{
			StartAnimationPanel(panelReady, "Hide", 1.0f);
		}


		void StartAnimationPanel (GameObject basePanel, string animationTriggerName, float startingCanvasAlpha)
		{		
			CanvasGroup canvGp = basePanel.GetComponent<CanvasGroup>();
			canvGp.alpha = startingCanvasAlpha;

			basePanel.SetActive(true);

			Animator animator = basePanel.GetComponent<Animator>();
			animator.SetTrigger(animationTriggerName);
		}



		// ------------------------------------------------------------------------------------


		void Update ()
		{
			// Misclick Penalty
			if (0.0f < penaltyJudgeTime)
			{
				penaltyJudgeTime -= Time.deltaTime;
				if (penaltyJudgeTime <= 0.0f)
				{
					penaltyJudgeTime = 0.0f;
					counterMisclick = 0;
				}
			}

		}


		// ------------------------------------------------------------------------------------

		public void SetTimerToHideReadyPanel (float waitTime)
		{
			StartCoroutine(StartTimerToHideReadyPanel(waitTime));
		}


		IEnumerator StartTimerToHideReadyPanel (float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			if (panelReady.activeSelf)
			{
				CanvasGroup canvGp = panelReady.GetComponent<CanvasGroup>();
				if (1f == canvGp.alpha)	HideReadyPanel();
			}
		}


		// ------------------------------------------------------------------------------------

		public void OnClickPanelReady ()
		{
			HideReadyPanel();
		}


		public void StartGame ()
		{
			panelReady.SetActive(false);
			timerCtrl.ResetTimer();
			timerCtrl.StartTimer();
		}


		// ------------------------------------------------------------------------------------

		public void OnClickPause ()
		{
			SoundManager.Instance.PlaySEButton1();
			ShowPausePanel();
		}


		void ShowPausePanel ()
		{
			objClearButtons.GetComponent<Animator>().enabled = false;
			StartAnimationPanel(panelPauseOverClear, "ShowPause", 0.0f);
			timerCtrl.StopTimer();
		}


		// ------------------------------------------------------------------------------------

		public void OnClickResume ()
		{
			SoundManager.Instance.PlaySEButton1();
			HidePausePanel();
		}


		void HidePausePanel ()
		{
			StartAnimationPanel(panelPauseOverClear, "HidePause", 1.0f);
			timerCtrl.StartTimer();
		}


		public void EndHidePauseAnimation ()
		{
			panelPauseOverClear.SetActive(false);
		}


		// ------------------------------------------------------------------------------------

		public void OnClickExit ()
		{
			if (gd.levelSelectSceneName == "")
			{
				Debug.LogWarning("LevelSlectSceneName is not specified.");
			}
			else
			{
				SoundManager.Instance.PlaySEButton2();
				SceneManager.LoadScene(gd.levelSelectSceneName);
			}
		}

		public void OnClickRetry ()
		{
			TripleTapGames.Instance.SetProgression(GameAnalyticsSDK.GAProgressionStatus.Start, GameData.Instance.currentLevel);
			SoundManager.Instance.PlaySEButton1();
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}


		// ------------------------------------------------------------------------------------

		public void ShowTimeIsUpPanel ()
		{
			SoundManager.Instance.PlaySETimeIsUp();
			objClearButtons.GetComponent<Animator>().enabled = false;
			StartAnimationPanel(panelPauseOverClear, "ShowTimeIsUp", 0.0f);
		}


		// ------------------------------------------------------------------------------------

		public void ShowClearPanel ()
		{		
			SoundManager.Instance.PlaySEGameClear();
			StartAnimationPanel(panelPauseOverClear, "ShowClear", 0.0f);
			objClearButtons.GetComponent<Animator>().enabled = true;

			//
			int stars = timerCtrl.GetNumberOfStars();
			int msec = timerCtrl.GetRemainingTimeMillsecond();
			gd.SaveGameClearData(stars, msec, out isClearTimeBest);
		}


		public void ShowClearTime ()
		{
			textClearTime.text = timerCtrl.FormatTimeRemainingForGameClear();

			panelPauseOverClear.GetComponent<Animator>().enabled = false;
			StartAnimationPanel(objClearTimeRemaining, "Show", 0.0f);
		}


		public void ShowClearStars ()
		{
			objClearStars.GetComponent<Animator>().SetInteger("Stars", timerCtrl.GetNumberOfStars());
			StartAnimationPanel(objClearStars, "Show", 0.0f);
		}


		public void ShowBestMark ()
		{
			if (isClearTimeBest)
			{
				objClearTimeRemaining.GetComponent<Animator>().SetTrigger("ShowBestMark");
			}
		}


		public void ShowClearButtons ()
		{
			Animator animator = objClearButtons.GetComponent<Animator>();
			animator.SetTrigger("Show");
		}


		// ------------------------------------------------------------------------------------

		public void Misclick (Vector3 position)
		{
			Debug.Log(penaltyClicks +"Misclick:" + counterMisclick.ToString() + " penaltyJudgeTime:"+penaltyJudgeTime.ToString("F2"));

			counterMisclick++;
			penaltyJudgeTime = penaltyResetTime;
			if (penaltyClicks <= counterMisclick && timerCtrl.isOn)
			{
				InstantiateMinusTimeMark(new Vector3(position.x, position.y, position.z));
				timerCtrl.MinusSec(penaltySeconds);
				SoundManager.Instance.PlaySEMis();
			}
		}


		void InstantiateMinusTimeMark (Vector3 position)
		{
			GameObject obj = Instantiate<GameObject>(prefabMinusTimeMark);
			obj.transform.LookAt(Camera.main.transform,Vector3.up);
			obj.transform.SetParent(this.transform, false);
			obj.transform.position = position;
		}

	}

}
