using System;

namespace VirtualDesktopSwitcher.Code
{
    public class Rectangle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Left => X;
        public int Top => Y;
        public int Right => X + Width;
        public int Bottom => Y + Height;

        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public void Set(string propertyName, int value)
        {
            var propertyInfo = GetType().GetProperty(propertyName.CapitalizeFirst());
            if (propertyInfo == null) throw new ArgumentException(nameof(propertyName));
            propertyInfo.SetValue(this, value);
        }
    }

    internal static class StringHelper
    {
        public static string CapitalizeFirst(this string text)
        {
            return char.ToUpper(text[0]) + text.Substring(1);
        }
    }
}
