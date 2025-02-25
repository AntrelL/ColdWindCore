using ColdWind.Core.GUIHelpers.Editor;
using System;
using UnityEditor;
using UnityEngine;

using Object = UnityEngine.Object;

namespace ColdWind.Core.SerializableInterface.Editor
{
    [CustomPropertyDrawer(typeof(SerializableInterface<>))]
    public class SerializableInterfaceDrawer : PropertyDrawer
    {
        private const string ObjectFieldName = "_object";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUIHelper.DrawInProperty(position, label, property, () =>
            {
                SerializedProperty objectFieldProperty = property.FindPropertyRelative(ObjectFieldName);
                EditorGUI.ObjectField(position, objectFieldProperty, typeof(Object), label);

                VerifyProperty(objectFieldProperty);
            });
        }

        private void VerifyProperty(SerializedProperty property)
        {
            if (property.objectReferenceValue == null)
                return;

            Type type = fieldInfo.FieldType.GetGenericArguments()[0];

            if (DoesImplementInterface(type, property, out Component suitableComponent))
                property.objectReferenceValue = suitableComponent;
            else
                property.objectReferenceValue = null;
        }

        private bool DoesImplementInterface(
            Type interfaceType, SerializedProperty property, out Component suitableComponent)
        {
            if (property.objectReferenceValue is GameObject gameObject)
                return DoesImplementInterface(interfaceType, gameObject, out suitableComponent);

            if (property.objectReferenceValue is Component component)
            {
                suitableComponent = component;
                return DoesImplementInterface(interfaceType, component);
            }

            suitableComponent = null;
            return false;
        }

        private bool DoesImplementInterface(
            Type interfaceType, GameObject gameObject, out Component suitableComponent)
        {
            foreach (var component in gameObject.GetComponents<Component>())
            {
                if (DoesImplementInterface(interfaceType, component))
                {
                    suitableComponent = component;
                    return true;
                }
            }

            suitableComponent = null;
            return false;
        }

        private bool DoesImplementInterface(Type interfaceType, Component component)
        {
            return interfaceType.IsAssignableFrom(component.GetType());
        }
    }
}
