/* GameData.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using System.Collections;

namespace HOGM
{
	public class GameData : MonoBehaviour
	{

		public enum ListItemType
		{
			Words,
			Silhouettes
		}

		public enum SearchType
		{
			Single,
			Pair,
			Similar
		}


		public string currentLevel;
		public int numberOfAnswers;
		public string levelSelectSceneName;
		public ListItemType currentListItemType;
		public SearchType currentSearchType;



		public static GameData Instance {get; private set;}


		void Awake ()
		{	
			// Singleton. (If instance is already set, destroy myself.)
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}
			else
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}

			
			
		}

        private void Start()
        {
			//TripleTapGames.Instance.Init();
		}


        // ------------------------------------------------------------------------------------

        public void SaveGameClearData (int stars, int msec, out bool isClearTimeBest)
		{
			string keySaveStars = "Level_"+ currentLevel + "_Stars"; // Levvel_?_Stars
			string keySaveTime  = "Level_"+ currentLevel + "_Msec";  // Levvel_?_Msec

			int savedStars = PlayerPrefs.GetInt(keySaveStars, 0);
			int savedTimeMsec = PlayerPrefs.GetInt(keySaveTime, 0);

			Debug.Log("stars:"+stars +" msec:"+msec +" savedStars:"+savedStars +" savedMsec:"+savedTimeMsec);

			//TripleTapGames.Instance.SetProgression(GameAnalyticsSDK.GAProgressionStatus.Complete, currentLevel,"","", stars);

			if (savedStars < stars)
			{
				PlayerPrefs.SetInt(keySaveStars, stars);
			}

			if (savedTimeMsec < msec)
			{
				PlayerPrefs.SetInt(keySaveTime, msec );
				isClearTimeBest = true;
			}
			else
			{
				isClearTimeBest = false;
			}
		}


		public void LoadGameClearData (string level, out int stars, out int msec)
		{
			string keySaveStars = "Level_"+ level + "_Stars";// Levvel_?_Stars
			string keySaveTime  = "Level_"+ level + "_Msec"; // Levvel_?_Msec

			stars = PlayerPrefs.GetInt(keySaveStars, 0);
			msec = PlayerPrefs.GetInt(keySaveTime, 0);
		}


		// ------------------------------------------------------------------------------------

		public bool IsCurrentListItemTypeWords ()
		{
			return currentListItemType == ListItemType.Words;
		}

		public bool IsCurrentListItemTypeSilhouettes ()
		{
			return currentListItemType == ListItemType.Silhouettes;
		}

		public bool IsCurrentSearchTypeSingle ()
		{
			return currentSearchType == SearchType.Single;
		}

		public bool IsCurrentSearchTypePair ()
		{
			return currentSearchType == SearchType.Pair;
		}

		public bool IsCurrentSearchTypeSimilar ()
		{
			return currentSearchType == SearchType.Similar;
		}

		public bool IsCurrentSearchTypeSingleOrPair()
		{
			return (currentSearchType == SearchType.Single || currentSearchType == SearchType.Pair);
		}



		// ------------------------------------------------------------------------------------

		// Utility

		public static void ShuffleArray<T> (T[] array)
		{
			System.Random rng = new System.Random();
			int n = array.Length;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				T tmp = array[k];
				array[k] = array[n];
				array[n] = tmp;
			}
		}
	}
}
