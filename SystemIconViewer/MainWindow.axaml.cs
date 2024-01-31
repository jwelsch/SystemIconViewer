using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace SystemIconViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            KeyDownEvent.AddClassHandler<TopLevel>(OnKeyDownHandler);
        }

        private void FindButton_Clicked(object? sender, RoutedEventArgs e)
        {
            FindSystemIcons();
        }

        private void OnKeyDownHandler(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FindSystemIcons();
            }
        }

        private void FindSystemIcons()
        {
            if (DataContext == null
                || DataContext is not MainViewModel model
                || _pathTextBox.Text == null)
            {
                return;
            }

            var options = new ImageListDrawOptions(_iconTransparentCheckBox.IsChecked ?? false,
                                                   _iconBlendCheckBox.IsChecked ?? false,
                                                   _iconSelectedCheckBox.IsChecked ?? false);

            model.FindSystemIcons(_pathTextBox.Text, options);
        }
    }
}