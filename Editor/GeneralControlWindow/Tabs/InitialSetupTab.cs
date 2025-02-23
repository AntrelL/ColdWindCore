using ColdWind.Core.Editor;
using ColdWind.Core.FolderArchitectureTool.Editor;
using UnityEditor;
using UnityEngine;

namespace ColdWind.Core.GeneralControlWindow.Editor
{
    public class InitialSetupTab : Tab
    {
        private const string FolderStructureName = "Folder Structure";
        private const string FolderArchitectureGeneratorName = "Folder Architecture Generator";

        private readonly Vector2 _windowSizeForTab = new(370, 140);

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
            int buttonWidth = 150;
            int smallIndent = 5;
            int standardIndent = 15;

            int protrudingPartPixels = 10;

            Rect areaRect = new(10, 40, 350, 80);
            Rect boxRect = areaRect.ResizeWithSavingPosition(protrudingPartPixels, protrudingPartPixels);

            GUI.Box(boxRect, string.Empty);
            GUILayout.BeginArea(areaRect);

            GUILayoutHelper.DrawHorizontallyInCenter(
                () => GUILayout.Label(FolderArchitectureGeneratorName, EditorStyles.boldLabel));

            GUILayout.Space(smallIndent);

            GUILayoutHelper.DrawHorizontallyInCenter(() =>
            {
                GUILayout.Label(FolderStructureName);
                GUILayout.Space(smallIndent);
                _folderStructure = (FolderStructure)EditorGUILayout.ObjectField(_folderStructure, typeof(FolderStructure), false);
            });

            GUILayout.Space(standardIndent);

            GUILayoutHelper.DrawInHorizontal(
                () => GUILayoutHelper.DrawBetweenSpaces(standardIndent, () =>
                {
                    GUILayoutHelper.DrawButton("Generate", 
                        () => FolderArchitectureGenerator.GenerateFolderStructure(_folderStructure), width: buttonWidth);

                    GUILayout.FlexibleSpace();

                    GUILayoutHelper.DrawButton($"Delete {FolderArchitectureGenerator.GitKeepFileName} Files",
                        () => FolderArchitectureGenerator.DeleteGitkeepFiles(), width: buttonWidth);
                }));

            GUILayout.EndArea();
        }
    }
}
