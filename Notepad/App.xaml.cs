using DevExpress.Mvvm;
using System.Reflection;
using System.Windows;

namespace Notepad
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ViewModelLocator.Default = new ViewModelLocator(Assembly.GetExecutingAssembly());
            Messenger.Default = new Messenger(true);
        }
    }
}
