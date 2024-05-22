using UnityEngine;
using UnityEditor;
using System;
using UnityEditorInternal;
using UnityEngine.UIElements;

namespace AP.Collections.Editor
{
    [CustomPropertyDrawer(typeof(EnumeratedCollection<,>), true)]
    public class EnumeratedCollectionDrawer : PropertyDrawer
    {
        private string _displayName;
        bool _foldout = true;
        private ReorderableList _reorderableList;
        bool _initialized = false;
        private void Init(SerializedProperty property)
        {
            if(_initialized) return;

            _reorderableList = new ReorderableList(property.serializedObject, property.FindPropertyRelative("_values"), true, true, true, true);
            

            _reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var lbl = new GUIContent(Enum.GetName(fieldInfo.FieldType.GetGenericArguments()[0], Enum.GetValues(fieldInfo.FieldType.GetGenericArguments()[0]).GetValue(index)).TrimStart('_').Replace('_', ' '));
                var element = _reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, element, lbl);
            };
            _reorderableList.elementHeightCallback = (int i) =>
            {
                var property = _reorderableList.serializedProperty.GetArrayElementAtIndex(i);
                if(!property.isExpanded)
                {
                    return EditorGUIUtility.singleLineHeight;
                }

                return EditorGUI.GetPropertyHeight(property) + 4;
            };
            _reorderableList.drawHeaderCallback = (Rect rect) =>
            {
                _foldout = EditorGUI.Foldout(rect, _foldout, _displayName, true);
            };
            _reorderableList.displayAdd = false;
            _reorderableList.displayRemove = false;

            _initialized = true;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);
            _displayName = label.text;
            var valuesProperty = property.FindPropertyRelative("_values");
            _reorderableList.serializedProperty = valuesProperty;
            if (valuesProperty != null && valuesProperty.isArray)
            {
                var enumType = fieldInfo.FieldType.GetGenericArguments()[0];
                var enumValues = Enum.GetValues(enumType);

                // Resize array if necessary
                if (valuesProperty.arraySize != enumValues.Length)
                {
                    valuesProperty.arraySize = enumValues.Length;
                    for (int i = 0; i < property.serializedObject.targetObjects.Length; i++)
                    {
                        if (PrefabUtility.IsPartOfPrefabInstance(property.serializedObject.targetObjects[i]))
                        {
                            PrefabUtility.RecordPrefabInstancePropertyModifications(property.serializedObject.targetObjects[i]);
                        }
                    }
                }
            }

            EditorGUI.BeginProperty(position, label, property);
            if (!_foldout)
            {
                ReorderableList.defaultBehaviours.DrawHeaderBackground(position);
                position.x += 6;
                _reorderableList.drawHeaderCallback.Invoke(position);
            }
            else
            {
                _reorderableList.DoList(position);
            }
            EditorGUI.EndProperty();
            
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Init(property);
            
            if (!_foldout)
            {
                return _reorderableList.headerHeight;
            }
            
            return _reorderableList.GetHeight();
        }
    }
}