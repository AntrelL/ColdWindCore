using ColdWind.Core.ConstantAddressables.Editor;
using ColdWind.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace ColdWind.Core.GeneralControlWindow.Editor
{
    public class ResourceControlTab : Tab
    {
        private const string ConstantAddressablesGenerator = "Constant Addressables Generator";
        private const string GenerateButtonName = "Generate";
        private const string OnEnterPlayModeToggleName = "On Enter Play Mode";
        private const string OnBuildToggleName = "On Build";

        private readonly Vector2 _windowSizeForTab = new(MainWindow.MinWidth, 130);
        private readonly Rect _constantAddressablesBlockSize = new(17, 40, 300, 70);

        private bool _isGenerateOnEnterPlayMode;
        private bool _isGenerateOnBuild;

        public override string Name => "Resource Control";

        public override void Open()
        {
            MainWindow.SetSize(_windowSizeForTab);

            _isGenerateOnEnterPlayMode = AddressableConstantsGenerator.IsGenerateOnEnterPlayMode;
            _isGenerateOnBuild = AddressableConstantsGenerator.IsGenerateOnBuild;
        }

        public override void Draw()
        {
            DrawConstantAddressablesBlock();
        }

        private void DrawConstantAddressablesBlock()
        {
            Rect boxRect = _constantAddressablesBlockSize.ResizeWithSavingPosition(
                GUILayoutHelper.StandardIndent, GUILayoutHelper.StandardIndent);

            GUI.Box(boxRect, string.Empty);
            GUILayout.BeginArea(_constantAddressablesBlockSize);

            GUILayoutHelper.DrawHorizontallyInCenter(
                () => GUILayout.Label(ConstantAddressablesGenerator, EditorStyles.boldLabel));

            GUILayout.Space(GUILayoutHelper.StandardIndent);

            GUILayoutHelper.DrawInHorizontal(
                () => GUILayoutHelper.DrawBetweenFlexibleSpaces(() =>
                {
                    GUILayoutHelper.DrawButton(GenerateButtonName,
                        () => AddressableConstantsGenerator.Generate(),
                        width: GUILayoutHelper.StandardButtonWidth);

                    GUILayout.Space(GUILayoutHelper.BigIndent);


                    GUILayoutHelper.DrawInVertical(() =>
                    {
                        GUILayoutHelper.DrawToggle(ref _isGenerateOnEnterPlayMode, OnEnterPlayModeToggleName,
                            (value) => AddressableConstantsGenerator.SetIsGenerateOnEnterPlayMode(value));

                        GUILayout.Space(GUILayoutHelper.SmallIndent);

                        GUILayoutHelper.DrawToggle(ref _isGenerateOnBuild, OnBuildToggleName,
                            (value) => AddressableConstantsGenerator.SetIsGenerateOnBuild(value));
                    });
                }));

            GUILayout.EndArea();
        }
    }
}
