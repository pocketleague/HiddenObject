using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class CTRHandView : MonoBehaviour
{
    public CTRHandConfig config;

    private RectTransform _canvasTransform;
    private RectTransform _rectTransform;
    private CanvasGroup   _canvasGroup;
    private Image         _handImage;

    private float _animateTimer;

    private void Awake()
    {
        _rectTransform   = GetComponent<RectTransform>();
        _canvasGroup     = GetComponent<CanvasGroup>();
        _handImage       = GetComponentInChildren<Image>();
        _canvasTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    private void Start()
    {
        SetUpHand();
    }

    private void Update()
    {
        HandleFade();
        HandleScale();
        HandleMovement();
    }

    private void HandleScale()
    {
        if ( !config.playTapAnimation )
            return;

        _animateTimer             = Mathf.Clamp01( _animateTimer + Time.deltaTime * ( Input.GetMouseButton( 0 ) ? config.scaleSpeed : -config.scaleSpeed ) );
        _rectTransform.localScale = Vector3.one * config.scale * Mathf.Lerp( 1f, config.scaleAmount, _animateTimer );
    }

    private void HandleFade()
    {
        if ( Input.GetMouseButton( 0 ) || config.alwaysVisible )
            _canvasGroup.alpha = Mathf.Clamp01( _canvasGroup.alpha + Time.deltaTime * config.fadeSpeed );
        else
            _canvasGroup.alpha = Mathf.Clamp01( _canvasGroup.alpha - Time.deltaTime * config.fadeSpeed );
    }

    private void HandleMovement()
    {
        _rectTransform.anchoredPosition = new Vector2( ( Input.mousePosition.x / Screen.width ) * _canvasTransform.sizeDelta.x, ( Input.mousePosition.y / Screen.height ) * _canvasTransform.sizeDelta.y );
    }

    private void SetUpHand()
    {
        _handImage.enabled = config.showHand;
        
        _rectTransform.anchorMin     = Vector2.zero;
        _rectTransform.anchorMax     = Vector2.zero;
        _rectTransform.localRotation = Quaternion.Euler( 0f, 0f, config.angle );
        _rectTransform.localScale    = Vector3.one * config.scale;

        _canvasGroup.alpha          = config.alwaysVisible ? 1f : 0f;
        _canvasGroup.interactable   = false;
        _canvasGroup.blocksRaycasts = false;

        _handImage.sprite = config.hand;
    }
}
