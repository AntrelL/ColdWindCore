using System;
using System.Collections.Generic;
using UnityEngine;

namespace ColdWind.Core.Editor
{
    public static class GUILayoutHelper
    {
        private const int UnspecifiedValue = -1;

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
