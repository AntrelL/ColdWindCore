using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ColdWind.Core.Editor
{
    public static class SettingsFileTool
    {
        public static void EditFile<T>(string folderPath, string fileName, Action<T> changer) 
            where T : ScriptableObject
        {
            var settings = GetFile<T>(folderPath, fileName);
            changer.Invoke(settings);
            SaveFile(settings);
        }
        
        public static T GetFile<T>(string folderPath, string fileName) where T : ScriptableObject
        {
            if (Directory.Exists(folderPath) == false)
                Directory.CreateDirectory(folderPath);

            string settingsFilePath = folderPath + fileName + Package.AssetFileExtension;
            T settings = AssetDatabase.LoadAssetAtPath<T>(settingsFilePath);

            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(settings, settingsFilePath);
            }

            return settings;
        }

        public static void SaveFile<T>(T file) where T : ScriptableObject
        {
            EditorUtility.SetDirty(file);
            AssetDatabase.SaveAssets();
        }
    }
}
