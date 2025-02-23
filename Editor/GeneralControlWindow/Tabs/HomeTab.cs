using ColdWind.Core.Editor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ColdWind.Core.GeneralControlWindow.Editor
{
    public class HomeTab : Tab
    {
        private const string VersionLoadingText = "Loading...";
        private const string VersionErrorText = "Error";
        private const string VersionLabelText = "Version: ";
        private const string TitleTextModules = "Modules";

        private const int NumberOfModuleColumns = 3;

        private readonly Vector2 _windowSizeForTab = new(MainWindow.MinWidth, 200);
        private readonly List<(string Name, bool State)> _moduleNames = new()
        {
            ("Модуль 1", false),
            ("Модуль 2", false),
            ("Модуль 3", true),
            ("Модуль 4", true),
            ("Модуль 5", false),
            ("Модуль 6", true),
        };

        private string _packageVersionText;

        public override string Name => "Home";

        public override void Initialize()
        {
            _packageVersionText = VersionLoadingText;
            UpdatePackageVersionText();
        }

        public override void Open()
        {
            MainWindow.SetSize(_windowSizeForTab);
        }

        public override void Draw()
        {
            GUILayoutHelper.DrawHorizontallyInCenter(
                () => GUILayout.Label(TitleTextModules, EditorStyles.boldLabel));

            GUILayout.Space(GUILayoutHelper.SmallIndent);
            GUILayoutHelper.DrawHorizontallyInCenter(() => DrawTableOfModules());

            GUILayout.FlexibleSpace();
            GUILayoutHelper.DrawHorizontallyInCenter(
                () => GUILayout.Label(VersionLabelText + _packageVersionText));
        }

        private void DrawTableOfModules()
        {
            int numberOfElementsInColumn = _moduleNames.Count / NumberOfModuleColumns;
            int numberOfRemainingModules = _moduleNames.Count % NumberOfModuleColumns;

            for (int i = 0; i < NumberOfModuleColumns; i++)
            {
                GUILayoutHelper.DrawInVertical(() =>
                {
                    int startIndex = i * numberOfElementsInColumn;
                    startIndex += Math.Min(i, numberOfRemainingModules);

                    int endIndex = startIndex + numberOfElementsInColumn;

                    if (i < numberOfRemainingModules)
                        endIndex++;

                    for (int j = startIndex; j < endIndex; j++)
                    {
                        GUILayoutHelper.DrawInHorizontal(() =>
                        {
                            GUILayout.Label(_moduleNames[j].Name);

                            GUILayoutHelper.DrawDisabled(
                                () => GUILayout.Toggle(_moduleNames[j].State, string.Empty));
                        });
                    }
                });

                if (i != NumberOfModuleColumns - 1)
                    GUILayout.Space(GUILayoutHelper.StandardIndent);
            }
        }

        private async void UpdatePackageVersionText()
        {
            _packageVersionText = await Package.GetVersionAsync() ?? VersionErrorText;
        }
    }
}
