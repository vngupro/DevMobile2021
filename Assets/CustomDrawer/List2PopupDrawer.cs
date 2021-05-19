using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class List2PopupAttribute : PropertyAttribute
{
    public Type myType;
    public string propertyName;
    public List2PopupAttribute(Type _myType, string _propertyName)
    {
        this.myType = _myType;
        this.propertyName = _propertyName;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(List2PopupAttribute))]
public class List2PopupDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        List2PopupAttribute atb = attribute as List2PopupAttribute;
        List<string> stringList = null;

        if (atb.myType.GetField(atb.propertyName) != null)
        {
            stringList = atb.myType.GetField(atb.propertyName).GetValue(atb.myType) as List<string>;
        }

        if (stringList != null && stringList.Count != 0)
        {
            int selectedIndex = Mathf.Max(stringList.IndexOf(property.stringValue), 0);
            selectedIndex = EditorGUI.Popup(position, property.name, selectedIndex, stringList.ToArray());
            property.stringValue = stringList[selectedIndex];
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
#endif