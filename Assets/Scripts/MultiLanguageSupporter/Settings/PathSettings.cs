using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System;
using System.IO;
#endif

namespace MultiLanguageSupporter.Settings
{
    [Serializable]
    public class PathSettings
    {
        [SerializeField]
        private string path;
        public string Path => path;
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(PathSettings))]
    public class PathSettingsDrawer : PropertyDrawer
    {
        private float EXTRA_HEIGHT = 24;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var serialPropPath = property.FindPropertyRelative("path");

            EditorGUI.BeginProperty(position, label, property);

            var guiContent = new GUIContent("Path to folder with .csv's:");
            GUI.enabled = false;

            var propRect = new Rect(position.x, position.y, position.width, 20);
            EditorGUI.PropertyField(propRect, serialPropPath, guiContent);
            GUI.enabled = true;

            var buttonRect = new Rect(position.x, position.y + 2 + propRect.height, position.width, 20);
            SetPath(buttonRect, serialPropPath);

            EditorGUI.EndProperty();
        }

        private void SetPath(Rect buttonRect, SerializedProperty serializedPath)
        {
            if (GUI.Button(buttonRect, "Open folder with CSVs"))
            {
                string openedPath = new DirectoryInfo(EditorUtility.OpenFolderPanel("Load ", "Assets", "Assets")).FullName;

                if (Directory.Exists(openedPath))
                {
                    string currDirectory = Directory.GetCurrentDirectory();
                    if (openedPath.Contains(currDirectory))
                    {
                        string inProjectPath = openedPath.Substring(currDirectory.Length + 1);
                        if (inProjectPath.Contains("Resources"))
                            serializedPath.stringValue = inProjectPath;
                        else
                            EditorUtility.DisplayDialog(
                                "Incorrect path", "Choosen path must contain the Resources folder!", "Ok");
                    }
                    else
                        EditorUtility.DisplayDialog("Incorrect path", "Choosen path not in the project!", "Ok");
                }
            }
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + EXTRA_HEIGHT;
        }
    }
#endif
}