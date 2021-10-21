using System;
using UnityEditor;
using UnityEngine;

namespace GameResources.Editor
{
    [CustomEditor(typeof(ResourceMap))]
    public class ResourceMapEditor : UnityEditor.Editor
    {
        private ResourceDatabase _resourceDatabase;
        
        private void OnEnable()
        {
            string[] guids = AssetDatabase.FindAssets("t:ResourceDatabase");
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            _resourceDatabase = AssetDatabase.LoadAssetAtPath<ResourceDatabase>(path);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            SerializedProperty width = serializedObject.FindProperty("_width");
            SerializedProperty height = serializedObject.FindProperty("_height");
            SerializedProperty resources = serializedObject.FindProperty("_resources");
            
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.DelayedIntField(width);
            EditorGUILayout.DelayedIntField(height);
            if (EditorGUI.EndChangeCheck())
            {
                RegenerateResources(width.intValue, height.intValue, resources);
            }

            for (int row = 0; row < height.intValue; row++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int column = 0; column < width.intValue; column++)
                {
                    SerializedProperty resourceProperty = resources.GetArrayElementAtIndex(row * width.intValue + column);
                    var resource = (Resource) resourceProperty.objectReferenceValue;
                    
                    bool isPressed = GUILayout.Button(new GUIContent(resource.Sprite.texture),
                        GUILayout.MaxWidth(100), GUILayout.MaxHeight(100), GUILayout.MinHeight(20), GUILayout.MinHeight(20));
                    if (isPressed)
                    {
                        ScrollResource(resourceProperty, resource.Type);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void ScrollResource(SerializedProperty resourceProperty, ResourceType resourceType)
        {
            int length = Enum.GetNames(typeof(ResourceType)).Length;
            int newValue = (int) Mathf.Repeat((int) resourceType + 1, length);
            newValue += newValue == 0 ? 1 : 0;
            resourceProperty.objectReferenceValue = _resourceDatabase.GetResourceByType((ResourceType) newValue);
        }

        private void RegenerateResources(int width, int height, SerializedProperty resources)
        {
            int elementsCount = width * height;
            while (resources.arraySize < elementsCount)
            {
                resources.InsertArrayElementAtIndex(resources.arraySize);
                SerializedProperty resource = resources.GetArrayElementAtIndex(resources.arraySize - 1);
                resource.objectReferenceValue = _resourceDatabase.GetResourceByType(ResourceType.Stone);
            }
            
            while (resources.arraySize > elementsCount)
            {
                resources.DeleteArrayElementAtIndex(resources.arraySize - 1);
            }
        }
        
    }
}