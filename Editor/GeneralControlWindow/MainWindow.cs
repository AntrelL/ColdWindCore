using ColdWind.Core.Editor;
using ColdWind.Core.GUIHelpers.Editor;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ColdWind.Core.GeneralControlWindow.Editor
{
    public class MainWindow : EditorWindow
    {
        public const int MinWidth = 337;

        private static List<Tab> s_tabs = new()
        { 
            new HomeTab(),
            new InitialSetupTab(),
            new ResourceControlTab()
        };

        private static string[] s_tabNames;
        private static MainWindow s_window;

        private int _selectedTabIndex = 0;
        private int _lastSelectedTabIndex = -1;

        private void OnEnable()
        {
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
        }

        private void OnDisable()
        {
            AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
        }

        private void OnGUI()
        {
            GUILayoutHelper.DrawBetweenSpaces(GUILayoutHelper.SmallIndent, GUILayoutHelper.MediumIndent,
                () => GUILayoutHelper.DrawInHorizontal(() =>
                {
                    _selectedTabIndex = GUILayout.Toolbar(_selectedTabIndex, s_tabNames);
                    GUILayout.FlexibleSpace();
                }));

            if (_selectedTabIndex != _lastSelectedTabIndex)
                s_tabs[_selectedTabIndex].Open();

            s_tabs[_selectedTabIndex].Draw();
            _lastSelectedTabIndex = _selectedTabIndex;
        }

        public static void SetSize(int width, int height) => SetSize(new Vector2(width, height));

        public static void SetSize(Vector2 size) => SetSize(size, size);

        public static void SetSize(Vector2 min, Vector2 max)
        {
            s_window.minSize = min;
            s_window.maxSize = max;
        }

        public static void Exit() => s_window.Close();

        [MenuItem("Window/" + Package.DisplayName)]
        private static void ShowWindow()
        {
            s_window = GetWindow<MainWindow>(Package.DisplayName);

            s_tabs.ForEach(tab => tab.Initialize());
            s_tabNames = s_tabs.Select(tab => tab.Name).ToArray();
        }

        private void OnBeforeAssemblyReload() => s_window.Close();
    }
}
