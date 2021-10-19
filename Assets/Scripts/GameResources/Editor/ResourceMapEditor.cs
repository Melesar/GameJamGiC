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
            string[] path = AssetDatabase.FindAssets("t:ResourceDatabase");
            _resourceDatabase = AssetDatabase.LoadAssetAtPath<ResourceDatabase>(path[0]);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            SerializedProperty width = serializedObject.FindProperty("_width");
            SerializedProperty height = serializedObject.FindProperty("_height");
            SerializedProperty resources = serializedObject.FindProperty("_resources");
            
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(width);
            EditorGUILayout.PropertyField(height);
            if (EditorGUI.EndChangeCheck())
            {
                RegenerateResources(width.intValue, height.intValue, resources);
            }

            for (int row = 0; row < height.intValue; row++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int column = 0; column < width.intValue; column++)
                {
                    var resource = (Resource) resources.GetArrayElementAtIndex(row * height.intValue + column)
                        .objectReferenceValue;
                    
                    EditorGUILayout.LabelField(new GUIContent(resource.Sprite.texture));
                }
                EditorGUILayout.EndHorizontal();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void RegenerateResources(int width, int height, SerializedProperty resources)
        {
            int elementsCount = width * height;
            while (resources.arraySize < elementsCount)
            {
                resources.InsertArrayElementAtIndex(resources.arraySize);
                SerializedProperty resource = resources.GetArrayElementAtIndex(resources.arraySize - 1);
                resource.objectReferenceValue = _resourceDatabase.GetResourceByType(ResourceType.Clay);
            }
            
            while (resources.arraySize > elementsCount)
            {
                resources.DeleteArrayElementAtIndex(resources.arraySize - 1);
            }
        }
        
    }
}