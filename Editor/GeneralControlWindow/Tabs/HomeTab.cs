using ColdWind.Core.Editor;
using ColdWind.Core.GUIHelpers.Editor;
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
        private const string TitleTextAuxiliaryTools = "AuxiliaryTools";

        private const int NumberOfModuleColumns = 3;
        private const int NumberOfAuxiliaryToolsColumns = 3;

        private readonly Vector2 _windowSizeForTab = new(580, 270);
        private readonly List<(string Name, bool State)> _modules = new()
        {
            ("General Control Window", true),
            ("Folder Architecture Tool", true),
            ("Advanced Debug Module", true),
            ("Constant Addressables", true),
            ("Serializable Interface", true),
            ("Modular Composite Root", true),
            ("Advanced Scene Manager", false),
            ("Universal UI", true),
            ("GUI Helpers", true),
            ("Resource Control Module", true),
            ("Game Object Control", true),
            ("Advanced Sounds", false),
            ("Advanced Animations", false),
            ("Advanced Effects", false),
            ("Event Links", true)
        };

        private readonly List<(string Name, bool State)> _auxiliaryTools = new()
        {
            ("Type Extensions", true),
            ("Additional Types", true),
            ("Package Info", true),
            ("File Generation Tool", true),
            ("Settings File Tool", true)
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
            DrawNamedComponentTable(TitleTextModules, NumberOfModuleColumns, _modules);

            GUILayout.Space(GUILayoutHelper.LargeIndent);

            DrawNamedComponentTable(TitleTextAuxiliaryTools, NumberOfAuxiliaryToolsColumns,
                _auxiliaryTools, GUILayoutHelper.HugeIndent);

            GUILayout.FlexibleSpace();
            GUILayoutHelper.DrawHorizontallyInCenter(
                () => GUILayout.Label(VersionLabelText + _packageVersionText));
        }

        private void DrawNamedComponentTable(
            string name, int numberOfColumns, List<(string Name, bool State)> components, int sideIndents = 0)
        {
            GUILayoutHelper.DrawHorizontallyInCenter(
                () => GUILayout.Label(name, EditorStyles.boldLabel));

            GUILayout.Space(GUILayoutHelper.SmallIndent + GUILayoutHelper.TinyIndent);

            GUILayoutHelper.DrawHorizontallyInCenter(
                () => DrawComponentTable(numberOfColumns, components, sideIndents));
        }

        private void DrawComponentTable(
            int numberOfColumns, List<(string Name, bool State)> components, int sideIndents)
        {
            GUILayoutHelper.DrawBetweenSpaces(sideIndents, 
                () => DrawComponentTable(numberOfColumns, components));
        }

        private void DrawComponentTable(int numberOfColumns, List<(string Name, bool State)> components)
        {
            int numberOfElementsInColumn = components.Count / numberOfColumns;
            int numberOfRemainingModules = components.Count % numberOfColumns;

            for (int i = 0; i < numberOfColumns; i++)
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
                            GUILayout.Label(components[j].Name);
                            GUILayout.FlexibleSpace();
                            GUIHelper.DrawDisabled(
                                () => GUILayout.Toggle(components[j].State, string.Empty));
                        });
                    }
                });

                if (i != numberOfColumns - 1)
                    GUILayout.Space(GUILayoutHelper.LargeIndent);
            }
        }

        private async void UpdatePackageVersionText()
        {
            _packageVersionText = await Package.GetVersionAsync() ?? VersionErrorText;
        }
    }
}
