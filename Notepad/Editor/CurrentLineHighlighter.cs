using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Rendering;
using System.Windows;
using System.Windows.Media;

namespace Notepad.Editor
{
    class CurrentLineHighlighter : IBackgroundRenderer
    {
        private readonly TextEditor _editor;

        public KnownLayer Layer => KnownLayer.Background;

        public CurrentLineHighlighter(TextEditor editor)
        {
            _editor = editor;

            _editor.TextArea.Caret.PositionChanged += (sender, e) =>
            {
                _editor.TextArea.TextView.InvalidateLayer(KnownLayer.Background);
            };
        }

        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            if (_editor.Document == null)
                return;

            textView.EnsureVisualLines();
            var currentLine = _editor.Document.GetLineByOffset(_editor.CaretOffset);

            foreach (var rect in BackgroundGeometryBuilder.GetRectsForSegment(textView, currentLine))
            {
                var brush = new SolidColorBrush(Color.FromRgb(16, 16, 16));
                var width = rect.Width > textView.ActualWidth ? rect.Width : textView.ActualWidth;

                var newRect = new Rect( 
                    new Point(rect.Location.X, rect.Location.Y),
                    new Size(width - 0.5, rect.Height)
                );

                drawingContext.DrawRectangle(brush, null, newRect);
            }
        }
    }
}
