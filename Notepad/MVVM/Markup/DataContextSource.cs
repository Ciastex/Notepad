using DevExpress.Mvvm;
using System;
using System.Windows.Markup;

namespace Notepad.MVVM.Markup
{
    [MarkupExtensionReturnType(typeof(ViewModelBase))]
    public class DataContextSource : MarkupExtension
    {
        [ConstructorArgument("viewModelType")]
        public Type ViewModelType { get; set; }

        public DataContextSource(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ViewModelLocator.Default.ResolveViewModel(ViewModelType.Name);
        }
    }
}
