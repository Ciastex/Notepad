using DevExpress.Mvvm;
using System.Windows.Media;

namespace Notepad.ViewModel
{
    public partial class EditorStatusBarViewModel : ViewModelBase
    {
        public SolidColorBrush Background { get; private set; }

        public string StatusText { get; private set; }
        public string LineColumnText { get; private set; }

        public EditorStatusBarViewModel()
        {
            Background = new SolidColorBrush(Color.FromRgb(0, 122, 204));

            Messenger.Default.Register<ChangeBackgroundColor>(this, (msg) =>
            {
                Background = new SolidColorBrush(msg.Color);
            });

            Messenger.Default.Register<ChangeStatusText>(this, (msg) =>
            {
                StatusText = msg.StatusText;
            });

            Messenger.Default.Register<ChangeLineColumnText>(this, (msg) =>
            {
                LineColumnText = msg.LineColumnText;
            });
        }
    }
}
