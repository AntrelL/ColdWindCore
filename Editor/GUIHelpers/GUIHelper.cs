using System;
using UnityEngine;

namespace ColdWind.Core.GUIHelpers.Editor
{
    public static class GUIHelper
    {
        public static void DrawDisabled(Action contentDrawer)
        {
            GUI.enabled = false;
            contentDrawer.Invoke();
            GUI.enabled = true;
        }
    }
}
