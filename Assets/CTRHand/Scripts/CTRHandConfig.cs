using UnityEngine;

[CreateAssetMenu( menuName = ( "Configs/CTRHandConfig" ), fileName = "CTRHandConfig" )]
public class CTRHandConfig : ScriptableObject
{
    [Header("Look")]
    public Sprite hand;
    public float  scale       = 1f;
    public float  angle       = 30f;
    public float  fadeSpeed   = 5f;

    [Header("Tap Animation")]
    public float  scaleAmount = 0.8f;
    public float  scaleSpeed  = 3f;

    [Header("Settings")]
    public bool showHand         = false;
    public bool alwaysVisible    = false;
    public bool playTapAnimation = true;
}
