using UnityEngine;

namespace ColdWind.Core
{
    public static class RectExtensions
    {
        private const int UnspecifiedValue = -1;

        public static Rect ResizeWithSavingPosition(
            this Rect rect, int widthChange = UnspecifiedValue, int heightChange = UnspecifiedValue)
        {
            if (widthChange != UnspecifiedValue)
            {
                rect.width += widthChange;
                rect.x -= widthChange / 2;
            }

            if (heightChange != UnspecifiedValue)
            {
                rect.height += heightChange;
                rect.y -= heightChange / 2;
            }

            return rect;
        }
    }
}
