using System;
using Scripts.UI;
using UnityEngine;

public class ScriptableInspectorWindow : MonoBehaviour
{
    private Window _parentWindow;

    public GameObject scrollViewObject;

    private void Awake()
    {
        _parentWindow = GetComponent<Window>();

        _parentWindow.Opened += OnWindowOpened;
        _parentWindow.Closed += OnWindowClosed;

        scrollViewObject.SetActive( false );
    }

    private void OnDestroy()
    {
        _parentWindow.Opened -= OnWindowOpened;
        _parentWindow.Closed -= OnWindowClosed;
    }

    private void OnWindowOpened()
    {
        scrollViewObject.SetActive( true );
    }

    private void OnWindowClosed()
    {
        scrollViewObject.SetActive( false );
    }
}
