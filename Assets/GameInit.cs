using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Events;
using UnityEngine.SceneManagement;
public class GameInit : MonoBehaviour
{

    public string coreSceneName = "Core";
    public GameObject sceneLoader;

    // Awake function from Unity's MonoBehavior
    void Awake()
    {
        Application.targetFrameRate = 60;
        InitSetUp();
    }

    void InitSetUp()
    {
        GameAnalytics.Initialize();

        GamesData.Instance.OnCustomDesignEvent += SetCustomDesignEvent;
        GamesData.Instance.OnDesignEvent += SetDesignEvent;
        GamesData.Instance.OnProgressionEvent += SetProgression;


        if (!FB.IsInitialized)
        {
           
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
           
        }

       
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            sceneLoader.SetActive(true);
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void SetProgression(GAProgressionStatus gAProgressionStatus, string progression01 = "", string progression02= "", string progression03= "", int score= -1)
    {
        Debug.Log(gAProgressionStatus.ToString() + " / " + progression01 + " / " + progression02 + " / " + progression03 + " / " + score);
        if(progression03 != "")
        {
            if (score== -1)
                GameAnalytics.NewProgressionEvent(gAProgressionStatus, progression01, progression02, progression03);
            else
                GameAnalytics.NewProgressionEvent(gAProgressionStatus, progression01, progression02, progression03, score);
        }
        else if (progression02 != "")
        {
            if (score == -1)
                GameAnalytics.NewProgressionEvent(gAProgressionStatus, progression01, progression02);
            else
                GameAnalytics.NewProgressionEvent(gAProgressionStatus, progression01, progression02,score);
        }
        else
        {
            if (score == -1)
                GameAnalytics.NewProgressionEvent(gAProgressionStatus, progression01);
            else
                GameAnalytics.NewProgressionEvent(gAProgressionStatus, progression01,score);
        }

    }

    private void SetDesignEvent(string eventName, float eventValue)
    {
        GameAnalytics.NewDesignEvent(eventName, eventValue);
    }

    private void SetCustomDesignEvent(string eventName, float eventValue, Dictionary<string, object> fields)
    {
        GameAnalytics.NewDesignEvent(eventName, eventValue, fields);
    }

    private void OnDisable()
    {
     //   TripleTapGames.Instance.OnInit -= InitSetUp;
        GamesData.Instance.OnCustomDesignEvent -= SetCustomDesignEvent;
        GamesData.Instance.OnDesignEvent -= SetDesignEvent;
        GamesData.Instance.OnProgressionEvent -= SetProgression;
    }


}
