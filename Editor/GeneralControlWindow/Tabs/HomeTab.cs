using ColdWind.Core.Editor;
using UnityEngine;

namespace ColdWind.Core.GeneralControlWindow.Editor
{
    public class HomeTab : Tab
    {
        private const string VersionLoadingText = "Loading...";
        private const string VersionErrorText = "Error";

        private readonly Vector2 _windowSizeForTab = new(200, 100);

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
            GUILayout.FlexibleSpace();
            GUILayoutHelper.DrawHorizontallyInCenter(
                () => GUILayout.Label("Version: " + _packageVersionText));
        }

        private async void UpdatePackageVersionText()
        {
            _packageVersionText = await Package.GetVersionAsync() ?? VersionErrorText;
        }
    }
}
