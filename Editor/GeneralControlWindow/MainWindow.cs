using ColdWind.Core.Editor;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ColdWind.Core.GeneralControlWindow.Editor
{
    public class MainWindow : EditorWindow
    {
        private static List<Tab> Tabs = new()
        { 
            new HomeTab()
        };

        private static string[] _tabNames;

        private int _selectedTabIndex = 0;

        [MenuItem("Window/" + Package.DisplayName)]
        private static void ShowWindow()
        {
            GetWindow<MainWindow>(Package.DisplayName);

            Tabs.ForEach(tab => tab.Initialize());
            _tabNames = Tabs.Select(tab => tab.Name).ToArray();
        }

        private void OnGUI()
        {
            _selectedTabIndex = GUILayout.Toolbar(_selectedTabIndex, _tabNames);
            Tabs[_selectedTabIndex].Draw();
        }
    }
}
