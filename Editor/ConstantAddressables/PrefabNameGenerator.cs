using ColdWind.Core.Editor;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;

namespace ColdWind.Core.ConstantAddressables.Editor
{
    public class PrefabNameGenerator
    {
        private const string PrefabExtension = ".prefab";
        private const string ClassName = "PrefabName";

        public static void Generate(
            AddressableAssetSettings addressableAssetSettings, string @namespace, string folderPath)
        {
            List<string> prefabAddresses = GetPrefabAddresses(addressableAssetSettings);
            Dictionary<string, string> constants = new();

            foreach (var prefabAddress in prefabAddresses)
            {
                string constantName = prefabAddress.Replace(PrefabExtension, string.Empty);
                constants.Add(constantName, prefabAddress);
            }

            FileGenerationTool.GenerateConstantClass(ClassName, @namespace, folderPath, constants);
        }

        private static List<string> GetPrefabAddresses(AddressableAssetSettings addressableAssetSettings)
        {          
            List<string> prefabAddresses = new();

            foreach (AddressableAssetGroup group in addressableAssetSettings.groups)
            {
                foreach (AddressableAssetEntry entry in group.entries)
                {
                    if (entry.AssetPath.EndsWith(PrefabExtension))
                        prefabAddresses.Add(entry.address);       
                }
            }

            return prefabAddresses;
        }    
    }
}
