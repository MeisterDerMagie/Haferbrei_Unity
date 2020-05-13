using System;
using System.Globalization;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;

namespace _Haferbrei{
[ OdinDrawer ]
public class DateTimeDrawer : OdinValueDrawer<DateTime>
{
    protected override void DrawPropertyLayout( IPropertyValueEntry<DateTime> entry, GUIContent label )
    {
        var _dateTime = entry.SmartValue;

        var _rect = EditorGUILayout.GetControlRect();

        if( label != null )
        {
            _rect = EditorGUI.PrefixLabel( _rect, label );
        }

        EditorGUI.BeginDisabledGroup(true);
        var _result = EditorGUI.TextField( _rect, _dateTime.ToString("dd.MM.yyyy HH:mm:ss"));
        if( DateTime.TryParse( _result, null, DateTimeStyles.RoundtripKind, out _dateTime ) )
        {
            entry.SmartValue = _dateTime;
        }
        EditorGUI.EndDisabledGroup();
    }
}
}