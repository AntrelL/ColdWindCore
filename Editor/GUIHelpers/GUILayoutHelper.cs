using System;
using System.Collections.Generic;
using UnityEngine;

namespace ColdWind.Core.GUIHelpers.Editor
{
    public static class GUILayoutHelper
    {
        public const int StandardButtonWidth = 150;

        public const int TinyIndent = 2;
        public const int SmallIndent = 5;
        public const int MediumIndent = 10;
        public const int LargeIndent = 15;
        public const int ExtraLargeIndent = 30;
        public const int HugeIndent = 50;
        public const int GiganticIndent = 100;

        private const int UnspecifiedValue = -1;

        public static void DrawToggle(ref bool value, string text, Action<bool> onValueChanged)
        {
            bool newValue = GUILayout.Toggle(value, text);

            if (newValue != value)
                onValueChanged.Invoke(newValue);

            value = newValue;
        }

        public static void DrawButton(
            string text, Action onClicked, int width = UnspecifiedValue, int height = UnspecifiedValue)
        {
            List<GUILayoutOption> options = new();

            if (width != UnspecifiedValue)
                options.Add(GUILayout.Width(width));

            if (height != UnspecifiedValue)
                options.Add(GUILayout.Height(height));

            if (GUILayout.Button(text, options.ToArray()))
                onClicked.Invoke();
        }

        public static void DrawBetweenSpaces(int pixelsForSpaces, Action contentDrawer)
        {
            DrawBetweenSpaces(pixelsForSpaces, pixelsForSpaces, contentDrawer);
        }

        public static void DrawBetweenSpaces(int pixelsForBeginSpace, int pixelsForEndSpace, Action contentDrawer)
        {
            GUILayout.Space(pixelsForBeginSpace);
            contentDrawer.Invoke();
            GUILayout.Space(pixelsForEndSpace);
        }

        public static void DrawInHorizontal(Action contentDrawer)
        {
            GUILayout.BeginHorizontal();
            contentDrawer.Invoke();
            GUILayout.EndHorizontal();
        }

        public static void DrawInVertical(Action contentDrawer)
        {
            GUILayout.BeginVertical();
            contentDrawer.Invoke();
            GUILayout.EndVertical();
        }

        public static void DrawBetweenFlexibleSpaces(Action contentDrawer)
        {
            GUILayout.FlexibleSpace();
            contentDrawer.Invoke();
            GUILayout.FlexibleSpace();
        }

        public static void DrawHorizontallyInCenter(Action contentDrawer)
        {
            DrawInHorizontal(
                () => DrawBetweenFlexibleSpaces(
                    () => contentDrawer.Invoke()));
        }
    }
}
