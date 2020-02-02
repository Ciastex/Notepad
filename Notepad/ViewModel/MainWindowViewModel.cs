using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using ICSharpCode.AvalonEdit;
using Notepad.Editor;
using System.Windows;
using System.Windows.Media;

namespace Notepad.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private TextEditor _textEditor;

        public string Title { get; private set; }
        public bool ShowLineNumbers { get; set; }
        public bool WrapWords { get; set; }

        public Thickness EditorPadding { get; set; }
        public FontFamily FontFamily { get; set; }
        public uint FontSize { get; set; }

        public MainWindowViewModel()
        {
            Title = "Notepad";
            ShowLineNumbers = true;

            FontSize = 12;
            FontFamily = new FontFamily("Hack");
            EditorPadding = new Thickness(4, 4, 4, 4);

            PropertyChanged += MainWindowViewModel_PropertyChanged;
        }

        [Command]
        public void InitializeEditor(RoutedEventArgs e)
        {
            _textEditor = e.OriginalSource as TextEditor;

            _textEditor.TextArea.TextView.BackgroundRenderers.Add(
                new CurrentLineHighlighter(_textEditor)
            );

            _textEditor.Options.ConvertTabsToSpaces = true;
            _textEditor.Options.CutCopyWholeLine = true;
            _textEditor.Options.IndentationSize = 2;

            _textEditor.TextArea.SelectionBrush = new SolidColorBrush(Color.FromRgb(43, 83, 125));
            _textEditor.TextArea.SelectionBorder = null;
            _textEditor.TextArea.SelectionCornerRadius = 0;

            _textEditor.TextArea.Caret.PositionChanged += (sender, e) =>
            {
                Messenger.Default.Send(
                    new EditorStatusBarViewModel.ChangeLineColumnText($"{_textEditor.TextArea.Caret.Line}:{_textEditor.TextArea.Caret.Column}")
                );
            };

            InitializeStatusBarDefaults();
        }

        private void InitializeStatusBarDefaults()
        {
            Messenger.Default.Send(
                new EditorStatusBarViewModel.ChangeStatusText("Ready")
            );

            Messenger.Default.Send(
                new EditorStatusBarViewModel.ChangeLineColumnText($"{_textEditor.TextArea.Caret.Line}:{_textEditor.TextArea.Caret.Column}")
            );
        }

        private void MainWindowViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ShowLineNumbers))
                if (ShowLineNumbers)
                {
                    EditorPadding = new Thickness(6, 6, 6, 6);
                    RaisePropertyChanged(nameof(EditorPadding));
                }
                else
                {
                    EditorPadding = new Thickness(4, 6, 6, 6);
                    RaisePropertyChanged(nameof(EditorPadding));
                }

            if (e.PropertyName == nameof(EditorPadding))
                _textEditor.TextArea.Padding = EditorPadding;
        }
    }
}
