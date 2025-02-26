using ColdWind.Core.GUIHelpers.Editor;
using UnityEditor;

namespace ColdWind.Core.ModularCompositeRoot.Editor
{
    [CustomEditor(typeof(CompositeRoot), true)]
    public class CompositeRootEditor : UnityEditor.Editor
    {
        private const string SpecialGroupsLabel = "Special Groups";
        private const string OtherGroupsFieldName = "_otherGroups";
        private const string ScriptFieldName = "m_Script";

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUIHelper.DrawDisabled(
                () => EditorGUILayout.PropertyField(serializedObject.FindProperty(ScriptFieldName)));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField(SpecialGroupsLabel, EditorStyles.boldLabel);

            DrawChildFields();

            EditorGUIHelper.DrawSpaces(2);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(OtherGroupsFieldName));

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawChildFields()
        {
            SerializedProperty serializedProperty = serializedObject.GetIterator();
            bool childFieldsLeft = true;

            while (serializedProperty.NextVisible(childFieldsLeft))
            {
                if (serializedProperty.propertyPath != ScriptFieldName
                    && serializedProperty.propertyPath != OtherGroupsFieldName)
                {
                    EditorGUILayout.PropertyField(serializedProperty, true);
                }

                childFieldsLeft = false;
            }
        }
    }
}
