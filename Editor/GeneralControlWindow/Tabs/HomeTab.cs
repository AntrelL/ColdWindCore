using ColdWind.Core.Editor;
using UnityEngine;

namespace ColdWind.Core.GeneralControlWindow.Editor
{
    public class HomeTab : Tab
    {
        private const string VersionLoadingText = "Loading...";
        private const string VersionErrorText = "Error";

        private string _packageVersionText;

        public override string Name => "Home";

        public override void Initialize()
        {
            _packageVersionText = VersionLoadingText;
            UpdatePackageVersionText();
        }

        public override void Draw()
        {
            GUILayout.Label("Version: " + _packageVersionText);
        }

        private async void UpdatePackageVersionText()
        {
            _packageVersionText = await Package.GetVersionAsync() ?? VersionErrorText;
        }
    }
}
