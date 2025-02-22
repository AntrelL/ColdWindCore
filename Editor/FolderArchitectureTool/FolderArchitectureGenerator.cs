using ColdWind.Core.AdvancedDebugModule;
using ColdWind.Core.Editor;
using System.IO;
using UnityEditor;

namespace ColdWind.Core.FolderArchitectureTool.Editor
{
    public class FolderArchitectureGenerator : EditorWindow
    {
        public const string GitKeepFileName = ".gitkeep";

        private const string DefaultFolderStructurePath = Package.Path +
            "Editor/FolderArchitectureTool/FolderStructures/Default.asset";

        public static FolderStructure GetDefaultFolderStructure()
        {
            return AssetDatabase.LoadAssetAtPath<FolderStructure>(DefaultFolderStructurePath);
        }

        public static void GenerateFolderStructure(FolderStructure folderStructure)
        {
            if (folderStructure == null)
            {
                AdvancedDebug.LogError<FolderArchitectureGenerator>(
                    "Data folder structure not set", isTypeName: false);

                return;
            }

            foreach (string folder in folderStructure.Folders)
            {
                if (Directory.Exists(folder) == false)
                    Directory.CreateDirectory(folder);

                File.WriteAllText(Path.Combine(folder, GitKeepFileName), string.Empty);
            }

            AssetDatabase.Refresh();

            AdvancedDebug.Log<FolderArchitectureGenerator>(
                "Folder structure generated successfully", isTypeName: false);
        }

        public static void DeleteGitkeepFiles()
        {
            string[] gitkeepFiles = Directory.GetFiles("Assets", GitKeepFileName, SearchOption.AllDirectories);

            foreach (string gitkeepFile in gitkeepFiles)
                File.Delete(gitkeepFile);
            
            AssetDatabase.Refresh();

            AdvancedDebug.Log<FolderArchitectureGenerator>(
                $"{GitKeepFileName} files deleted successfully", isTypeName: false);
        }
    }
}
