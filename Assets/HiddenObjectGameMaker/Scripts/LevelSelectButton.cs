/* LevelSelectButton.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace HOGM
{
	public class LevelSelectButton : MonoBehaviour
	{

		[SerializeField]
		string level;
		[SerializeField]
		int answers;

		[SerializeField]
		string gameSceneName;

		[SerializeField]
		GameData.ListItemType listItemType;
		[SerializeField]
		GameData.SearchType searchType;

		[SerializeField, Space(15)]
		Text textLevelNumber;
		[SerializeField]
		Image[] stars;
		[SerializeField]
		Text textBestLabel;
		[SerializeField]
		Text textBestTime;
		[SerializeField]
		Text textListItemType;
		[SerializeField]
		Text textSearchType;
		[SerializeField]
		Color colorStarOn;
		[SerializeField]
		Color colorStarOff;



		void Start ()
		{		
			LoadGameClearDataAndDraw();
			DrawTypes();
		}
			

		void LoadGameClearDataAndDraw ()
		{
			int savedStars = 0;
			int savedTimeMsec = 0;
			GameData.Instance.LoadGameClearData(level, out savedStars, out savedTimeMsec);

			//
			textLevelNumber.text = level;

			if (savedTimeMsec == 0)
			{
				foreach (Image imageStar in stars)
				{
					imageStar.gameObject.SetActive(false);
				}
				textBestLabel.gameObject.SetActive(false);
				textBestTime.gameObject.SetActive(false);
			}
			else
			{
				for (int i=0; i<stars.Length; i++)
				{
					stars[i].gameObject.SetActive(true);
					if (i < savedStars)
					{
						stars[i].color = colorStarOn;
					}
					else
					{
						stars[i].color = colorStarOff;
					}
				}
				textBestLabel.gameObject.SetActive(true);
				textBestTime.gameObject.SetActive(true);

				int sec100 = (savedTimeMsec % 1000) / 10;
				int totalSec = savedTimeMsec / 1000;
				int totalMin = totalSec / 60;
				int sec = totalSec - totalMin*60;
				textBestTime.text = string.Format("<b>{0:D2}:{1:D2}</b><size=30>.{2:D2}</size>", totalMin, sec, sec100);
			}
		}


		void DrawTypes ()
		{
			textListItemType.text = listItemType.ToString();
			textSearchType.text = searchType.ToString();
		}


		public void OnClick ()
		{
			SoundManager.Instance.PlaySEButton1();

			GameData gd = GameData.Instance;

			gd.currentLevel = level;
			gd.numberOfAnswers = answers;
			gd.levelSelectSceneName = SceneManager.GetActiveScene().name;
			gd.currentListItemType = listItemType;
			gd.currentSearchType = searchType;

			TripleTapGames.Instance.SetProgression(GameAnalyticsSDK.GAProgressionStatus.Start, level);

			SceneManager.LoadScene(gameSceneName);
		}
	}
}
