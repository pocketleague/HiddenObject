using UnityEngine;

public class ScriptableInspectorList : MonoBehaviour
{
    public ScriptableInspectorConfig      scriptableInspectorConfig;

    public ScriptableInspectorNameWidget  namePrefab;
    public ScriptableInspectorFieldWidget fieldPrefab;

    private Transform _transformCached;

    private void Awake()
    {
        _transformCached = transform;
    }

    void Start()
    {
        DrawFields();
    }

    private void DrawFields()
    {
        foreach ( var config in scriptableInspectorConfig.configs ) {
            var fields = config.GetType().GetFields();

            Instantiate( namePrefab, _transformCached, false ).SetUp( config.name );
            foreach ( var field in fields ) {
                Instantiate( fieldPrefab, _transformCached, false ).SetUp( field, config );
            }
        }
    }
}
