using System;
using System.Reflection;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class ScriptableInspectorFieldWidget : MonoBehaviour
{
    public TextMeshProUGUI label;
    public TMP_InputField  field;

    private FieldInfo     _targetField;
    private object        _targetObject;
    private TypeConverter _converter;

    private void Awake()
    {
        field.onValueChanged.AddListener( OnValueChanged );
    }

    private void OnDestroy()
    {
        field.onValueChanged.RemoveListener( OnValueChanged );
    }

    public void SetUp( FieldInfo targetField, object targetObject )
    {
        _targetField  = targetField;
        _targetObject = targetObject;
        _converter    = TypeDescriptor.GetConverter( _targetField.FieldType );

        label.SetText             ( targetField.Name );
        field.SetTextWithoutNotify( targetField.GetValue( targetObject ).ToString() );
    }

    private void OnValueChanged( string newValue )
    {
        try {
            object res = _converter.ConvertFromString( newValue );
            _targetField.SetValue( _targetObject, res );
        } catch ( Exception ) { }
    }
}
