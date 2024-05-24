using TMPro;
using UnityEngine;

public class ScriptableInspectorNameWidget : MonoBehaviour
{
    public TextMeshProUGUI _label;

    public void SetUp( string name )
    {
        _label.SetText( name );
    }
}
