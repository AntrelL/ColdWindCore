using ColdWind.Core.AdvancedDebugModule;
using ColdWind.Core.Editor;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace ColdWind.Core.ConstantAddressables.Editor
{
    public class AddressableConstantsGenerator : IPreprocessBuildWithReport
    {
        private const string SettingsFileName = nameof(ConstantAddressablesSettings);

        private static readonly string s_constantAddressablesNamespace = string.Join(".",
            new string[] { nameof(ColdWind), nameof(Core), nameof(ConstantAddressables) });

        private static readonly string s_folderPath = 
            $"{Package.PathForDynamicData}ConstantAddressables/";

        public static bool IsGenerateOnEnterPlayMode { get; private set; }

        public static bool IsGenerateOnBuild { get; private set; }

        public int callbackOrder => 0;

        static AddressableConstantsGenerator()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

            var settings = SettingsFileTool.GetFile<ConstantAddressablesSettings>(s_folderPath, SettingsFileName);

            IsGenerateOnEnterPlayMode = settings.IsGenerateOnEnterPlayMode;
            IsGenerateOnBuild = settings.IsGenerateOnBuild;
        }

        public static void SetIsGenerateOnEnterPlayMode(bool value)
        {
            SettingsFileTool.EditFile<ConstantAddressablesSettings>(s_folderPath, SettingsFileName, 
                (settings) => settings.IsGenerateOnEnterPlayMode = value);

            IsGenerateOnEnterPlayMode = value;
        }

        public static void SetIsGenerateOnBuild(bool value)
        {
            SettingsFileTool.EditFile<ConstantAddressablesSettings>(s_folderPath, SettingsFileName,
                (settings) => settings.IsGenerateOnBuild = value);

            IsGenerateOnBuild = value;
        }

        public static void Generate()
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

            if (settings == null)
            {
                AdvancedDebug.LogError<AddressableConstantsGenerator>(
                    "AddressableAssetSettings not found. Please generate addressables settings");

                return;
            }

            if (Directory.Exists(s_folderPath) == false)
                Directory.CreateDirectory(s_folderPath);
            
            PrefabNameGenerator.Generate(settings, s_constantAddressablesNamespace, s_folderPath);
            AddressableLabelGenerator.Generate(settings, s_constantAddressablesNamespace, s_folderPath);

            AssetDatabase.Refresh();
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (IsGenerateOnBuild)
                Generate();
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (IsGenerateOnEnterPlayMode && state == PlayModeStateChange.ExitingEditMode)
                Generate();
        }
    }
}
