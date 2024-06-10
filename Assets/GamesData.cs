using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameAnalyticsSDK;

public class GamesData 
{
    public Action OnInit;
    public Action<string, float> OnDesignEvent;
    public Action<string, float, Dictionary<string, object>> OnCustomDesignEvent;
    public Action<GAProgressionStatus, string, string, string, int> OnProgressionEvent;


    public void Init()
    {
        OnInit?.Invoke();
    }

    public void SetDesignEvent(string eventName, float eventValue)
    {
        OnDesignEvent?.Invoke(eventName, eventValue);
    }

    public void SetCustomDesignEvent(string eventName, float eventValue, Dictionary<string, object> fields)
    {
        OnCustomDesignEvent?.Invoke(eventName, eventValue, fields);
    }

    public void SetProgression(GAProgressionStatus gAProgressionStatus, string progression01 = "", string progression02 = "", string progression03 = "", int score = -1)
    {
        OnProgressionEvent?.Invoke(gAProgressionStatus, progression01, progression02, progression03, score);
    }

    public static GamesData Instance
    {
        get
        {
            if(smInstance == null)
            {
                smInstance = new GamesData();
            }

            return smInstance;

        }
    }

    public static GamesData smInstance;
}
