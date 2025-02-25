using ColdWind.Core.FolderArchitectureTool.Editor;
using ColdWind.Core.GUIHelpers.Editor;
using UnityEditor;
using UnityEngine;

namespace ColdWind.Core.GeneralControlWindow.Editor
{
    public class InitialSetupTab : Tab
    {
        private const string FolderStructureName = "Folder Structure";
        private const string FolderArchitectureGeneratorName = "Folder Architecture Generator";
        private const string GenerateButtonName = "Generate";
        private const string DeleteGitKeepFilesButtonName = 
            "Delete " + FolderArchitectureGenerator.GitKeepFileName + " Files";

        private readonly Vector2 _windowSizeForTab = new(370, 140);
        private readonly Rect _folderArchitectureBlockSize = new(10, 40, 350, 80);

        private FolderStructure _folderStructure;

        public override string Name => "Initial Setup";

        public override void Open()
        {
            MainWindow.SetSize(_windowSizeForTab);
            _folderStructure = FolderArchitectureGenerator.GetDefaultFolderStructure();
        }

        public override void Draw()
        {
            DrawFolderArchitectureBlock();
        }

        private void DrawFolderArchitectureBlock()
        {
            Rect boxRect = _folderArchitectureBlockSize.ResizeWithSavingPosition(
                heightChange: GUILayoutHelper.MediumIndent);

            GUI.Box(boxRect, string.Empty);
            GUILayout.BeginArea(_folderArchitectureBlockSize);

            GUILayoutHelper.DrawHorizontallyInCenter(
                () => GUILayout.Label(FolderArchitectureGeneratorName, EditorStyles.boldLabel));

            GUILayout.Space(GUILayoutHelper.SmallIndent);

            GUILayoutHelper.DrawHorizontallyInCenter(() =>
            {
                GUILayout.Label(FolderStructureName);
                GUILayout.Space(GUILayoutHelper.SmallIndent);
                _folderStructure = (FolderStructure)EditorGUILayout.ObjectField(
                    _folderStructure, typeof(FolderStructure), false);
            });

            GUILayout.Space(GUILayoutHelper.LargeIndent);

            GUILayoutHelper.DrawInHorizontal(
                () => GUILayoutHelper.DrawBetweenSpaces(GUILayoutHelper.LargeIndent, () =>
                {
                    GUILayoutHelper.DrawButton(GenerateButtonName, 
                        () => FolderArchitectureGenerator.GenerateFolderStructure(_folderStructure),
                        width: GUILayoutHelper.StandardButtonWidth);

                    GUILayout.FlexibleSpace();

                    GUILayoutHelper.DrawButton(DeleteGitKeepFilesButtonName,
                        () => FolderArchitectureGenerator.DeleteGitkeepFiles(),
                        width: GUILayoutHelper.StandardButtonWidth);
                }));

            GUILayout.EndArea();
        }
    }
}
