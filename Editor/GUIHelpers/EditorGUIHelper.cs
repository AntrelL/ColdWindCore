using System;
using UnityEditor;
using UnityEngine;

namespace ColdWind.Core.GUIHelpers.Editor
{
    public static class EditorGUIHelper
    {
        public static void DrawInProperty(
            Rect totalPosition, GUIContent label, SerializedProperty property, Action contentDrawer)
        {
            EditorGUI.BeginProperty(totalPosition, label, property);
            contentDrawer.Invoke();
            EditorGUI.EndProperty();
        }
    }
}
