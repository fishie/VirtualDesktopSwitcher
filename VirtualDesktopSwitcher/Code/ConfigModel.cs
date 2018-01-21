using System.Collections.Generic;

namespace VirtualDesktopSwitcher.Code
{
    public class ConfigModel
    {
        public bool TaskViewScroll;
        public bool DesktopScroll;
        public bool VirtualBoxFix;
        public bool HideOnStartup;
        public List<Rectangle> Rectangles = new List<Rectangle>();
    }
}
