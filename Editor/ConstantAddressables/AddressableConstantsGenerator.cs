using ColdWind.Core.AdvancedDebugModule;
using ColdWind.Core.Editor;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace ColdWind.Core.ConstantAddressables.Editor
{
    [InitializeOnLoad]
    public class AddressableConstantsGenerator : IPreprocessBuildWithReport
    {
        private static readonly string s_constantAddressablesNamespace = string.Join(".",
            new string[] { nameof(ColdWind), nameof(Core), nameof(ConstantAddressables) });

        private static readonly string s_folderPath = 
            $"{Application.dataPath}/{Package.DisplayNameInPascalCase}/ConstantAddressables/";

        public int callbackOrder => 0;

        static AddressableConstantsGenerator()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        public void OnPreprocessBuild(BuildReport report) => UpdatePrefabsInfo();

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
                UpdatePrefabsInfo();
        }

        [MenuItem("Tools/Constant Addressables/Update Info")]
        private static void UpdatePrefabsInfo()
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

            if (settings == null)
            {
                AdvancedDebug.LogError<AddressableConstantsGenerator>("AddressableAssetSettings not found");
                return;
            }

            if (Directory.Exists(s_folderPath) == false)
                Directory.CreateDirectory(s_folderPath);
            
            PrefabNameGenerator.Generate(settings, s_constantAddressablesNamespace, s_folderPath);
            AddressableLabelGenerator.Generate(settings, s_constantAddressablesNamespace, s_folderPath);

            AssetDatabase.Refresh();
        }
    }
}
