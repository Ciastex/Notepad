using System.Windows.Media;

namespace Notepad.ViewModel
{
    public partial class EditorStatusBarViewModel
    {
        public class ChangeBackgroundColor
        {
            public Color Color { get; }

            public ChangeBackgroundColor(Color color)
            {
                Color = color;
            }
        }

        public class ChangeStatusText
        {
            public string StatusText { get; }

            public ChangeStatusText(string statusText)
            {
                StatusText = statusText;
            }
        }

        public class ChangeLineColumnText
        {
            public string LineColumnText { get; }

            public ChangeLineColumnText(string lineColumnText)
            {
                LineColumnText = lineColumnText;
            }
        }
    }
}
