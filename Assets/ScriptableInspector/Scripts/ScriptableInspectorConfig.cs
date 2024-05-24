using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "ScriptableInspectorConfig", menuName = "ScriptableInspector/ScriptableInspectorConfig Config" )]
public class ScriptableInspectorConfig : ScriptableObject
{
    public List<ScriptableObject> configs;
}
