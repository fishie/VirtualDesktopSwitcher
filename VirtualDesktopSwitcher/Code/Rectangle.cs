namespace VirtualDesktopSwitcher
{
    class Rectangle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Left { get { return X; } }
        public int Top { get { return Y; } }
        public int Right { get { return X + Width; } }
        public int Bottom { get { return Y + Height; } }

        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public void Set(string propertyName, int value)
        {
            GetType().GetProperty(propertyName.CapitalizeFirst()).SetValue(this, value);
        }

        public int Get(string propertyName)
        {
            return (int)GetType().GetProperty(propertyName.CapitalizeFirst()).GetValue(this);
        }
    }

    static class StringHelper
    {
        public static string CapitalizeFirst(this string text)
        {
            return char.ToUpper(text[0]) + text.Substring(1);
        }
    }
}
