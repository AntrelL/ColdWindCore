using ColdWind.Core.Editor;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.AddressableAssets.Settings;

namespace ColdWind.Core.ConstantAddressables.Editor
{
    public class AddressableLabelGenerator
    {
        private const string ClassName = "AddressableLabel";

        public static void Generate(
            AddressableAssetSettings addressableAssetSettings, string @namespace, string folderPath)
        {
            List<string> labels = GetLabels(addressableAssetSettings);
            Dictionary<string, string> constants = new();

            labels.ForEach(label => constants.Add(label, label));

            FileGenerationTool.GenerateConstantClass(ClassName, @namespace, folderPath, constants);
        }

        private static List<string> GetLabels(AddressableAssetSettings addressableAssetSettings)
        {
            HashSet<string> labels = new();

            foreach (var group in addressableAssetSettings.groups)
                foreach (var entry in group.entries)
                    foreach (var label in entry.labels)
                        labels.Add(label);

            return labels.ToList();
        }
    }
}
